   M S B A M L   `   `   `  ����I  EHciMiniProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:HciMiniProjectHciMiniProject  5   	   U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5           HciMiniProject.TableWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:HciMiniProject     5   	   Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5        � Title$  GDPer��5
   	   $	��600��5   	   $
��1000��6   $	��300��6#   $	��500��63    � 
ResizeMode$ 	CanResize=�5   	   .��5      �  � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $
��0.3* �6   �� 5      $
��8.5* �6   �� 5      $
��0.3* �6    � ColumnDefinitions 5   
   �� 5      $
��0.3* �6   �� 5      $��3* �6   �� 5      $��3* �6   �� 5      $
��4.5* �6   �� 5      $��6* �6   �� 5      $
��0.3* �6   .+�5    
   �� $��0��6   $��6��6!   ��5!      �� 5"       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5#      �  � Color$ #ededed��6#    � Offset$ 0Q�63   � 5$      $ white��6#   $ 1Q�61   5%      5'      �� 5)   
   $��TABLE OF GDP��6   $��0��6(   $��2��65   $��1��6G   $��20,10,20,10q�6X   $��20(�6m   6}   &   System.Windows.Controls.DataGrid  5+   
   -   5          Name$ 	DataTable��5+      	   CanUserSortColumns	 . 
   AutoGenerateColumns
 .  $��2��5.      $��5��5/      $��14(�50      $��18,0,0,23.6q�6!   $��2��66   ��5-      �� *tableRowsData+ � 	Resources 51      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value��%,  '        �� 52      "  6   .�53      ��  � ��
 � ���6D   �� 54       � ��$
 Bold"�63   6B   �� 55       � ��
 �   �6D   �� 56       � ��$ 
0, 0, 1, 2q�68   6M   �� 57       � ��$ Center=�6C   6T   �� 58       � ��$ 25(�61   6>   59      5:         Columns 5<      0  *System.Windows.Controls.DataGridTextColumn  5=      ,  &System.Windows.Controls.DataGridColumn   Header$
 Date��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 63   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *date+6X     5>      $ Value��6%   $ * 64    6>   �� *value+   ElementStyle 5?      �� 5@      " ��6     �� Triggers 5A      �� 5B   "    �� Binding 6.   �� *barChart.minValue+.a�5C   &   ��  � ��
 � ���6C   5D   #   �� 5E   "    6.   �� *barChart.maxValue+.a�5F   &   ��  � ��
 �  ��6C   5G   #   5I      5K      5M      5O      5P      