using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using Newtonsoft.Json;

namespace WpfApplication1.Utils
{
    public class ApiClient
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string apiKey = "adaptive"; // Replace with your actual API key

        static ApiClient()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("x-api-key", apiKey); // Add the API key to the headers
        }

        public static async Task<List<Broker>> GetAllBrokers()
        {
            try
            {
                //HttpResponseMessage response = await client.GetAsync("http://oao-be.koyeb.app/brokers");
                HttpResponseMessage response = await client.GetAsync("http://oao-be.koyeb.app/broker/get?url=http://oao-be.koyeb.app/broker/receive");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                ApiResponse3<Broker> apiResponse = JsonConvert.DeserializeObject<ApiResponse3<Broker>>(responseBody);

                if (apiResponse.meta.code == 200)
                {
                    // print data from API (only broker name)
                    // foreach (var broker in apiResponse.data)
                    // {
                    //     Console.WriteLine(broker.nama);
                    // }
                    return apiResponse.data;
                }
                else
                {
                    MessageBox.Show($"Error: {apiResponse.meta.message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return new List<Broker>();
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Request error: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Broker>();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Unexpected error: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Broker>();
            }
        }

        public static async Task<List<Customer2>> GetAllCustomers()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://oao-be.koyeb.app/inpo");
                Console.WriteLine(response.StatusCode);
                response.EnsureSuccessStatusCode();
                if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Data tidak ditemukan", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return new List<Customer2>();
                }
                
                if(response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    MessageBox.Show("Terjadi kesalahan pada server", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return new List<Customer2>();
                }
                
                if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show("Unauthorized", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return new List<Customer2>();
                }
                
                string responseBody = await response.Content.ReadAsStringAsync();
                ApiResponse3<Customer2> apiResponse = JsonConvert.DeserializeObject<ApiResponse3<Customer2>>(responseBody);

                if (apiResponse.meta.code == 200)
                {
                    // print data from API (only broker name)
                    foreach (var customer in apiResponse.data)
                    {
                        Console.WriteLine(customer.Nama);
                    }
                    // print count of data
                    return apiResponse.data;
                }
                else
                {
                    MessageBox.Show($"Error: {apiResponse.meta.message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return new List<Customer2>();
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Request error: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Customer2>();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Unexpected error: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Customer2>();
            }
        }

        public static async Task<bool> CreateCustomerRegist1(CustemerRegist1 custemer)
        {
            try
            {
                string json = JsonConvert.SerializeObject(custemer);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("http://oao-be.koyeb.app/customer/regist", content);
                response.EnsureSuccessStatusCode();
                //HttpResponseMessage response = await client.PostAsync("http://localhost:8080/customer/regist", content);
                //response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                //string responseBody = @"
                //{
                //    'meta': {
                //        'code': 201,
                //        'message': 'Created'
                //    },
                //    'data': {
                //        'customer': 'yusufjana28@gmail.com',
                //        'brokerName': 'PT Amantara Sekuritas Indonesia'
                //    }
                //}";


                ApiResponse<CustomerData> apiResponse = JsonConvert.DeserializeObject<ApiResponse<CustomerData>>(responseBody);
                //MessageBox.Show(apiResponse.ToString());

                //ApiResponse<object> apiResponse = JsonConvert.DeserializeObject<ApiResponse<object>>(responseBody);

                if (apiResponse.meta.code == 201)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show($"Error: {apiResponse.meta.message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Request error: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception e)
            {
                //MessageBox.Show("Berhasil!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                MessageBox.Show($"Unexpected error: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public static async Task<bool> CreateCustomerRegist2(Customer customer)
        {
            try
            {
                // Serialize customer data to JSON, ensuring DateTime values are in ISO-8601 format
                string json = JsonConvert.SerializeObject(new
                {
                    nik = customer.nik,
                    nama = customer.nama,
                    alamat = customer.alamat,
                    kelurahan = customer.kelurahan,
                    kecamatan = customer.kecamatan,
                    provinsi = customer.provinsi,
                    kota = customer.kota,
                    tanggal_lahir = customer.tanggal_lahir?.ToString("yyyy-MM-ddTHH:mm:ss.fff'Z'", CultureInfo.InvariantCulture), // ISO-8601 format with timezone
                    tempat_lahir = customer.tempat_lahir,
                    jenis_kelamin = customer.jenis_kelamin,
                    golongan_darah = customer.golongan_darah,
                    status_pernikahan = customer.status_pernikahan,
                    rt_rw = customer.rt_rw,
                    agama = customer.agama,
                    kewarganegaraan = customer.kewarganegaraan,
                    pekerjaan = customer.pekerjaan,
                    tanggal_berlaku = customer.tanggal_berlaku?.ToString("yyyy-MM-ddTHH:mm:ss.fff'Z'", CultureInfo.InvariantCulture), // ISO-8601 format with timezone
                    foto = customer.foto,
                    tanda_tangan = customer.tanda_tangan,
                    ktp_capture = customer.ktp_capture,
                    tanggal_input = customer.tanggal_input?.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK"), // ISO-8601 format with timezone
                });
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("http://oao-be.koyeb.app/customer/fill", content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                ApiResponse<CustomerRegistData> apiResponse = JsonConvert.DeserializeObject<ApiResponse<CustomerRegistData>>(responseBody);

                if (apiResponse.meta.code == 201)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show($"Error: {apiResponse.meta.message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Request error: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception e)
            {
                //MessageBox.Show("Berhasil!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                MessageBox.Show($"Unexpected error: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }


        public static async Task<bool> CreateCustomer(Customer customer)
        {
            try
            {
                string json = JsonConvert.SerializeObject(customer);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("http://oao-be.koyeb.app/customer", content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                ApiResponse<object> apiResponse = JsonConvert.DeserializeObject<ApiResponse<object>>(responseBody);
                Console.WriteLine(apiResponse);

                if (apiResponse.meta.code == 201)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show($"Error: {apiResponse.meta.message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Request error: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Unexpected error: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }


    public class CustomerData
    {
        //public string customer { get; set; }
        public int idBroker { get; set; }
        public string brokerName { get; set; }
    }

    public class CustomerRegistData
    {
        public CustomerRegist2 customer { get; set; }
        public CustomerTransaction customerTransaction { get; set; }
        public string kode_otp { get; set; }
    }


    public class ApiResponse3<T>
    {
        public Meta meta { get; set; }
        public List<T> data { get; set; }
    }

    public class ApiResponse<T>
    {
        public Meta meta { get; set; }
        public T data { get; set; }
    }


    public class Meta
    {
        public int code { get; set; }
        public string message { get; set; }
    }
}
