   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   BackgroundColor  StandardColor  
HoverColor  PressedColor  DialogBackgroundColor  ScollBarBackgroundStart  ScollBarBackgroundEnd  StandardBrush  �� Color  
HoverBrush 	 BackgroundBrush 
 ScollBarBackgroundBrush �� 
StartPoint �� EndPoint � Offset � Color  ScrollButtons �� 
TargetType �� Property �� Value �� 
TargetType  ScrollThumbs	 �� RadiusX
 �� RadiusY �� Triggers P� Property P� Value �� 
TargetName  MyScrollBar �Name � RowDefinitions ��Name F�Name ]�Name ]� Thumb n�Name  ButtonDefaultBrush ]� IncreaseRepeatButton J� Opacity ]� DecreaseRepeatButton P� 
SourceName  LayerChild1Brush �� Angle�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      5      8d2http://schemas.microsoft.com/expression/blend/2008  5      Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      %!  &	        &	      &	 >     &	 _     &	 ~     &	 �     &	 �     &	 �     7  &	 �     7  &		     7   &	
 )    7  7  &	 �    &	 �    7 	 &	 l    7  7  7  7  7  7  7  7  7  ��5      #AE2F33��6+   ��5      #800000��6)   ��5      	#AAC64D45��6(   ��5      #AA0000��6(   ��5	      	#FF666666��63   ��5
      	#77C64D45��65   ��5      	#99C64D45��63   �� 5      8    6*   �� 5      8    6'   �� 5      8    6,   �� 5      $	 0,0&�68   $	 1,0&�6I   ��5      � $ 0Q�6   8   6    � 5      $ 1Q�6   8  6    5      �� 5      " ��6    .�5      ��  � |� .  6,   �� 5       � �� 5      �� 5   
   " ��6   .R�5      �� $��Border��6   
������ ._�5      �� $��ContentPresenter��6   $��Center=�67   $��Center=�6T   #��z��5      #��z��5      #��z��5      5      5       5"      5#      �� 5$      " n�6   .�5%      ��  � ��8   6+   �� 5&       � �� 5'      �� 5(   
   " n�6   .R�5)      � $
��Grid��6   .+�5*      �� $
��Auto��6   $
��Auto��6%   $��Stretch=�63   $��Stretch=�6Q   
������ 5+      �� 5,      $��
Rectangle1��6   $��7��6*   $
��Auto��64   $��Stretch=�6B   $��Stretch=�6`   #��z��5-      $	 4��6D   $
 4��6P   5.       5/      P� 50       �@�Tag$ 
Horizontal��66   . �51      �� $ 
Rectangle1��6    � ��$
 Auto��6Q   �� 52      $ 
Rectangle1��6    � ��$ 7��6R   53      55      57      58      �� 59      " ��6   .�5:      ��  �@��IsFlicksEnabled .  6/   �� 5;       � ��# � �6+   �� 5<       � ��# � 
 6+   �� 5=       � ��$ 12��6/   �� 5>       � �� 5?      �� 5@   
   " ��6   .R�5A      � $ GridRoot��6   #��� ��6#   #��z��5B       5C      �� 5D      $��18��6   �� 5E      $��0.00001* �6   �� 5F      $��18��6   .+�5H      �� $ DecreaseRepeat��6   $��ScrollBar.LineUpCommand��63   8��  #���  5I   G   .��5J      F� $ DecreaseArrow��6   $��Center=�6,   $��Center=�6I   +�����g5     �    ��j ���j ��j ��g5     8�� 5K   9   5L      ]� 5M      $ 
PART_Track��6   $��1��6(   |�.  ��.  5N      n� 5O      $ Thumb��6   #���  6'   #���  5P       5R      �� 5S      $ PageUp��6   $��ScrollBar.PageDownCommand��6/   |�.  $ 0Q�6e    5U      �� 5V      $ PageDown��6   $��ScrollBar.PageUpCommand��61   |�.  $ 0Q�6e   5X      �� 5Y      $ IncreaseRepeat��6   $��2��63   $��ScrollBar.LineDownCommand��6@   #���  5Z      #���  6H   .��5[      F� $ IncreaseArrow��6   $��Center=�6,   $��Center=�6I   +�����g5 ��j ���j     �        ��g5 ��j 8�� 5\   9   5]      5^       5_      P� 5`      $ IncreaseRepeat��6    �@J�IsMouseOver . . �5a      �� $ IncreaseArrow��6   8   � ��6V   5b      P� 5c      $ DecreaseRepeat��6    �@J�IsMouseOver . . �5d      �� $ DecreaseArrow��6   8   � ��6V   5e      P� 5f      $ Thumb��6    �@J�IsMouseOver . . �5g      �� $ Thumb��6   8   � ��6N   5h      P� 5i      $ Thumb��6    �@n�
IsDragging . . �5j      �� $ Thumb��6   8   � ��6Q   5k      P� 5l      $ IncreaseRepeat��6    � �� . . �5m      �� $ IncreaseArrow��6   8   � ��6Y   5n      P� 5o      $ DecreaseRepeat��6    � �� . . �5p      �� $ DecreaseArrow��6   8   � ��6Y   5q      P� 5r       � {� .  . �5s      �� $ IncreaseArrow��6   # �  62    � ��6]   �� 5t      $ DecreaseArrow��6   # �  62    � ��6]   �� 5u      $ Thumb��6    � y�$ 	Collapsed=�6J   5v      P� 5w       �@��Orientation$ 
Horizontal=�67   . �5x      �� $ GridRoot��6    �@�LayoutTransform 5y      �� 5z      $	 -90Q�6$   5|      �� 5}      $ 
PART_Track��6    �@�LayoutTransform 5~      �� 5      $	 -90Q�6$   5�      �� 5�       � ��$
 Auto��69   �� 5�       � ��$ 12��6:   �� 5�      $ Thumb��6    �@�Tag$ 
Horizontal��6J   �� 5�      $ DecreaseRepeat��6    � ��$ ScrollBar.LineLeftCommand��6Q   �� 5�      $ IncreaseRepeat��6    � ��$  ScrollBar.LineRightCommand��6Q   �� 5�      $ PageDown��6    � ��$ ScrollBar.PageLeftCommand��6K   �� 5�      $ PageUp��6    � ��$  ScrollBar.PageRightCommand��6I   5�      5�      5�      5�      5�      