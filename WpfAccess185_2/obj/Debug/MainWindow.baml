   M S B A M L   `   `   `  ����I  EWpfAccess185_2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:WpfAccess185_2WpfAccess185_2  5   *      WpfAccess185_2.MainWindow   6	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    6-   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6o   @d2http://schemas.microsoft.com/expression/blend/2008     6�   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6�   .localclr-namespace:WpfAccess185_2     6*  -   5           base_window#��[  5        � Title$  Магазин��5       � WindowStartupLocation$ CenterScreen=�5      .��5      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5	      $��* �6   .+�5   
   �   
additional#��[ 6   �� 5   
   $��0��6O     base_textblock#��[ 6\    �� TextAlignment$
 Left=�5      $
��Left=�6   $��100 0q�61   ��5      ��  �� ElementName$ 	MainFrame�� �� Path$ Content.Title�5   1   �� 5   
   $��0��6   $��Resources/computer.png��6   $
��Left=�6=   $��5q�6X    � 5   
   -   5          �Name$ 	MainFrame��5      $��1��6#     � NavigationUIVisibility$ Hidden=�60   5      �� 5   
   $��0��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6<   .+�5      �� -   5        	 ��Name$	 BtnUser��5      $��Пользователь��6&     base_button#��[ 5      $
��Auto��6'   64   �� 5      -   5        $	 BtnAdmin��5      $��
Админ��6'   #��[ 5      $
��Auto��6'   64   �� 5      -   5        $	 BtnCoordinator��5      $��Координатор��6-   #��[ 5      $
��Auto��6&   63   �� 5      -   5        $	 BtnBack��5      $��
Назад��6&   #��[ 5      $
��Auto��6&   64   5      5      5      