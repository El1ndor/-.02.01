   M S B A M L   `   `   `  ����I  EWpfAccess185_2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WpfAccess185_2.PagesWpfAccess185_2.Pages  5   �   (    "WpfAccess185_2.Pages.SellGoodsPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    64   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     6H   4local"clr-namespace:WpfAccess185_2.Pages     6�     N� Title$  Продажи��5         	base_page#��[  6   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��300 �6   �� 5	      $��* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��2��6.   $��Stretch=�6B   .+�5      �� $!��Выберите товар��6     base_textblock#��[ 5      �� 5      -   5         ��Name$ 
ComboGoods��5   5   $��Stretch=�6    �� SelectedValuePath$ GoodId��5   /    �� DisplayMemberPath$ GoodName��5      5      � 5   
   -   5         �Name$ GridGood��5      $	��300��6!   $��1��6-   $��0��6:    5      �� 5      $��8* �6#   �� 5      $��7* �6#    5      �� 5      $	��310 �6    �� 5      $
��auto �6    �� 5      $��20 �6    .+�5       �� $	��240��6   $��0��6    $��Uniform=�6-   $��Center=�5!      $��2��5#      $
��30,0q�6   ��5"      ��  �� Path$ GetPhoto�5#      �� 5$      $��1��6   $	��100��6&   $��2��63   .+�5%      �� 	 �� Padding$	 3q�6   $
��Auto��6(   $��14(�5'      
 �� TextAlignment$
 Center=�5(      $��Stretch=�5)      $
��Wrap=�5*      #��[ 5+      ��5&      �� *
GoodName+5+      �� 5,      $
��Auto��5-      $��Center=�5.      $��Center=�5/       
 akcent#��[ 50      $
��Bold"�51      $��20(�6   #��[ 52      ��5,      �� *Price+ �� StringFormat$ c�� �� ConverterCulture$ ru-RU��52      53      54      &   System.Windows.Controls.DataGrid  55   
   -   5          Name$ DataGridSells��55         AutoGenerateColumns .  $��1��6F   $��1��6S      Columns 56      0  *System.Windows.Controls.DataGridTextColumn  57      ,  &System.Windows.Controls.DataGridColumn   Header$ Дата продажи��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 58      �� *
DateSell+$ g��$ ru-RU��  5:      $ Количество��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 5;       6   �� *	SellCount+6   5=      5>      5?      