using libComm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Xml;

namespace WpfApplication1.Utils
{
    public class UtilFile
    { 
      private UtilLog log = new UtilLog();
      private UtilImageText utilimage = new UtilImageText();
      private UtilImageText util = new UtilImageText();

      public bool checkFolderExist(string path, ref string _folderpath)
      {
        bool flag;
        try
        {
          string path1 = path + "\\" + DateTime.Now.ToString("yyyyMMdd");
          _folderpath = path1;
          if (!Directory.Exists(path1))
            Directory.CreateDirectory(path1);
          flag = true;
        }
        catch (Exception ex)
        {
          flag = false;
        }
        return flag;
      }

      public void generateFile(string isifile, string path, DateTime date)
      {
        string _folderpath = "";
        string path1 = "";
        string str = "";
        string[] strArray = isifile.Split('|');
        try
        {
          if (!this.checkFolderExist(path, ref _folderpath))
            return;
          if (_folderpath != "")
          {
            str = "NIK|NAMA|TEMPATLAHIR|TGLLAHIR|JNSKELAMIN|ALAMAT|RTRW|KEL|KEC|AGAMA|STATUSKAWIN|PEKERJAAN|KEWARGANEGARAAN|BERLAKU|PROPINSI|GOLDARAH|TGLINPUT";
            path1 = _folderpath + "\\" + strArray[1] + "_" + strArray[0] + "_" + date.ToString("yyyyMMddHHmmss") + ".txt";
          }
          if (!File.Exists(path1))
          {
            File.Create(path1).Dispose();
            using (TextWriter textWriter = (TextWriter) new StreamWriter(path1))
            {
              textWriter.WriteLine(str);
              textWriter.WriteLine(isifile);
              textWriter.Close();
            }
          }
        }
        catch (Exception ex)
        {
          throw ex;
        }
      }

      public void generateFileService(
        List<PropertyService> data,
        string path,
        DateTime date,
        string nikInput,
        string status,
        ref string pathfile)
      {
        string _folderpath = "";
        string path1 = "";
        string str1 = "";
        string str2 = "";
        int num1 = 0;
        foreach (PropertyService propertyService in data)
        {
          str2 += propertyService.name;
          if (propertyService.name == "NAMA_LGKP")
            str1 = propertyService.value;
          if (num1 < data.Count<PropertyService>() - 1)
          {
            str2 += "|";
            ++num1;
          }
        }
        string str3 = "";
        int num2 = 0;
        foreach (PropertyService propertyService in data)
        {
          str3 += propertyService.value.ToUpper();
          if (num2 < data.Count<PropertyService>() - 1)
          {
            str3 += "|";
            ++num2;
          }
        }
        string str4 = "NIK_INPUT|STATUS|" + str2;
        string str5 = nikInput + "|" + status + "|" + str3;
        try
        {
          if (!this.checkFolderExist(path, ref _folderpath))
            return;
          if (_folderpath != "")
          {
            path1 = _folderpath + "\\" + str1 + "_" + nikInput + "_" + date.ToString("yyyyMMddHHmmss") + "_WS.txt";
            pathfile = path1;
          }
          if (!File.Exists(path1))
          {
            File.Create(path1).Dispose();
            using (TextWriter textWriter = (TextWriter) new StreamWriter(path1))
            {
              textWriter.WriteLine(str4);
              textWriter.WriteLine(str5);
              textWriter.Close();
            }
          }
        }
        catch (Exception ex)
        {
          throw ex;
        }
      }

      public void generateFileALLService(
        List<PropertyService> data,
        string path,
        DateTime date,
        string nikInput,
        string status,
        ref string pathfile)
      {
        string _folderpath = "";
        string path1 = "";
        string str1 = "";
        string str2 = "";
        int num1 = 0;
        foreach (PropertyService propertyService in data)
        {
          str2 += propertyService.name;
          if (propertyService.name == "NAMA_LGKP")
            str1 = propertyService.value;
          if (num1 < data.Count<PropertyService>() - 1)
          {
            str2 += "|";
            ++num1;
          }
        }
        string str3 = "";
        int num2 = 0;
        foreach (PropertyService propertyService in data)
        {
          str3 += propertyService.value.ToUpper();
          if (num2 < data.Count<PropertyService>() - 1)
          {
            str3 += "|";
            ++num2;
          }
        }
        string str4 = "NIK_INPUT|STATUS|" + str2;
        string str5 = nikInput + "|" + status + "|" + str3;
        if (status == "TIDAK DITEMUKAN")
        {
          str4 = "NIK_INPUT|STATUS";
          str5 = nikInput + "|" + status;
        }
        if (nikInput == string.Empty)
          str5 = "";
        try
        {
          if (!this.checkFolderExist(path, ref _folderpath))
            return;
          if (_folderpath != "")
          {
            path1 = _folderpath + "\\" + date.ToString("yyyyMMddHHmmss") + "_ALL_WS.txt";
            pathfile = path1;
          }
          if (!File.Exists(path1))
          {
            File.Create(path1).Dispose();
            using (TextWriter textWriter = (TextWriter) new StreamWriter(path1))
            {
              textWriter.WriteLine(str4);
              textWriter.WriteLine(str5);
              textWriter.Close();
            }
          }
          else
            File.AppendAllText(path1, string.Format("{0}{1}", (object) str5, (object) Environment.NewLine));
        }
        catch (Exception ex)
        {
          throw ex;
        }
      }

      public void generateFileAll(List<MD_KTP> data, string path, DateTime date)
      {
        string _folderpath = "";
        string path1 = "";
        try
        {
          if (!this.checkFolderExist(path, ref _folderpath))
            return;
          if (_folderpath != "")
            path1 = _folderpath + "\\" + date.ToString("yyyyMMddHHmmss_ALL") + ".txt";
          if (!File.Exists(path1))
          {
            File.Create(path1).Dispose();
            using (TextWriter textWriter = (TextWriter) new StreamWriter(path1))
            {
              string str1 = "NIK|NAMA|TEMPATLAHIR|TGLLAHIR|JNSKELAMIN|ALAMAT|RTRW|KEL|KEC|AGAMA|STATUSKAWIN|PEKERJAAN|KEWARGANEGARAAN|BERLAKU|PROPINSI|GOLDARAH|TGLINPUT";
              textWriter.WriteLine(str1);
              foreach (MD_KTP mdKtp in data)
              {
                string str2 = mdKtp.NIK.Trim() + "|" + mdKtp.NAMA.Trim() + "|" + mdKtp.TEMPATLAHIR.Trim() + "|" + DateTime.Parse(mdKtp.TGLLAHIR.Trim(), (IFormatProvider) CultureInfo.CreateSpecificCulture("fr-FR")).ToShortDateString() + "|" + mdKtp.JNSKELAMIN.Trim() + "|" + mdKtp.ALAMAT.Trim() + "|" + mdKtp.RTRW.Trim() + "|" + mdKtp.KEL.Trim() + "|" + mdKtp.KEC.Trim() + "|" + mdKtp.AGAMA.Trim() + "|" + mdKtp.STATUSKAWIN.Trim() + "|" + mdKtp.PEKERJAAN.Trim() + "|" + mdKtp.KEWARGANEGARAAN.Trim() + "|" + mdKtp.BERLAKU.Trim() + "|" + mdKtp.PROPINSI.Trim() + "|" + mdKtp.GOLDARAH.Trim() + "|" + mdKtp.TGLINPUT.Trim() + Environment.NewLine;
                textWriter.Write(str2);
              }
              textWriter.Close();
            }
          }
        }
        catch (Exception ex)
        {
          throw ex;
        }
      }

      public void generateXML(string folderpath, DateTime date, DataEktp data)
      {
        string _folderpath = "";
        string path = "";
        try
        {
          if (!this.checkFolderExist(folderpath, ref _folderpath))
            return;
          if (folderpath != "")
            path = _folderpath + "\\" + data.getNama() + "_" + data.getNik() + "_" + date.ToString("yyyyMMddHHmmss") + ".xml";
          if (!File.Exists(path))
          {
            FileIOPermission fileIoPermission = new FileIOPermission(FileIOPermissionAccess.AllAccess, path);
            using (FileStream output = new FileStream(path, FileMode.Create))
            {
              using (XmlWriter xmlWriter = XmlWriter.Create((Stream) output))
              {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("ktpel");
                xmlWriter.WriteElementString("NIK", data.getNik());
                xmlWriter.WriteElementString("NAMA", data.getNama());
                string[] strArray = data.getTtl().Split(',');
                xmlWriter.WriteElementString("TEMPATLAHIR", strArray[0]);
                xmlWriter.WriteElementString("TGLLAHIR", strArray[1]);
                xmlWriter.WriteElementString("JNSKELAMIN", data.getJenisKelamin());
                xmlWriter.WriteElementString("GOLDARAH", data.getGolDarah());
                xmlWriter.WriteElementString("ALAMAT", data.getAlamat());
                xmlWriter.WriteElementString("RTRW", data.getRtRw());
                xmlWriter.WriteElementString("KEL", data.getKelDesa());
                xmlWriter.WriteElementString("KEC", data.getKecamatan());
                xmlWriter.WriteElementString("AGAMA", data.getAgama());
                xmlWriter.WriteElementString("STATUSKaWIN", data.getStatusPerkawinan());
                xmlWriter.WriteElementString("PEKERJAAN", data.getPekerjaan());
                xmlWriter.WriteElementString("KEWARGANEGARAAN", data.getKewarganegaraan());
                xmlWriter.WriteElementString("BERLAKU", data.getBerlakuHingga());
                xmlWriter.WriteElementString("PROVINSI", data.getProvinsi());
                xmlWriter.WriteElementString("FOTO", this.utilimage.ByteArrayToHexString(data.getPoto()));
                xmlWriter.WriteElementString("TANDATANGAN", this.utilimage.ByteArrayToHexString(data.getTandatangan()));
                xmlWriter.WriteElementString("TGLINPUT", date.ToString());
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
              }
            }
          }
        }
        catch (Exception ex)
        {
          this.log.LogError(ex);
        }
      }

      public void generateXMLAll(string folderpath, DateTime date, List<MD_KTP> data)
      {
        string _folderpath = "";
        string path = "";
        int num = 0;
        try
        {
          if (!this.checkFolderExist(folderpath, ref _folderpath))
            return;
          if (folderpath != "")
            path = _folderpath + "\\" + date.ToString("yyyyMMddHHmmss_ALL") + ".xml";
          if (!File.Exists(path))
          {
            FileIOPermission fileIoPermission = new FileIOPermission(FileIOPermissionAccess.AllAccess, path);
            using (FileStream output = new FileStream(path, FileMode.Create))
            {
              using (XmlWriter xmlWriter = XmlWriter.Create((Stream) output))
              {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("ktpel");
                foreach (MD_KTP mdKtp in data)
                {
                  ++num;
                  xmlWriter.WriteStartElement("KTP");
                  xmlWriter.WriteElementString("NO", Convert.ToString(num));
                  xmlWriter.WriteElementString("NIK", mdKtp.NIK);
                  xmlWriter.WriteElementString("NAMA", mdKtp.NAMA);
                  xmlWriter.WriteElementString("TEMPATLAHIR", mdKtp.TEMPATLAHIR);
                  xmlWriter.WriteElementString("TGLLAHIR", DateTime.Parse(mdKtp.TGLLAHIR, (IFormatProvider) CultureInfo.CreateSpecificCulture("fr-FR")).ToShortDateString());
                  xmlWriter.WriteElementString("JNSKELAMIN", mdKtp.JNSKELAMIN);
                  xmlWriter.WriteElementString("GOLDARAH", mdKtp.GOLDARAH);
                  xmlWriter.WriteElementString("ALAMAT", mdKtp.ALAMAT);
                  xmlWriter.WriteElementString("RTRW", mdKtp.RTRW);
                  xmlWriter.WriteElementString("KEL", mdKtp.KEL);
                  xmlWriter.WriteElementString("KEC", mdKtp.KEC);
                  xmlWriter.WriteElementString("AGAMA", mdKtp.AGAMA);
                  xmlWriter.WriteElementString("STATUSKaWIN", mdKtp.STATUSKAWIN);
                  xmlWriter.WriteElementString("PEKERJAAN", mdKtp.PEKERJAAN);
                  xmlWriter.WriteElementString("KEWARGANEGARAAN", mdKtp.KEWARGANEGARAAN);
                  xmlWriter.WriteElementString("BERLAKU", mdKtp.BERLAKU);
                  xmlWriter.WriteElementString("PROVINSI", mdKtp.PROPINSI);
                  xmlWriter.WriteElementString("FOTO", this.utilimage.ByteArrayToHexString(mdKtp.FOTO));
                  xmlWriter.WriteElementString("TANDATANGAN", this.utilimage.ByteArrayToHexString(mdKtp.TANDATANGAN));
                  xmlWriter.WriteElementString("TGLINPUT", date.ToString());
                  xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                xmlWriter.Close();
              }
            }
          }
        }
        catch (Exception ex)
        {
          this.log.LogError(ex);
        }
      }
    }
}