   M S B A M L   `   `   `  ����I  EWpfAccess185_2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:WpfAccess185_2WpfAccess185_2  5   +       WpfAccess185_2.LoginWindow   6	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    6.   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6p   @d2http://schemas.microsoft.com/expression/blend/2008     6�   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6�   .localclr-namespace:WpfAccess185_2     6+    � Title$  Вход��5         base_window#��[  5      $	��240��5      $	��400��6   $	��220��5      $	��400��6    � WindowStartupLocation$ CenterScreen=�5       � 
ResizeMode$ NoResize=�5      .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��120 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $��40 �6   �� 5      $��40 �6   �� 5      $��* �6   .+�5   
   � $��0��6   $��0��6   $��2��6,     
additional#��[ 6@   �� 5   
   $��0��6   $��0��6   $��2��5      $��Resources/computer.png��5      $
��Left=�5      $��10q�6   �� 5   
   $��0��6   $��0��6!   $��2��61   $ ��Вход в систему��5        header_textblock#��[ 5      �� 5   
   $��
Логин��6   $��1��6!   $��0��6.     base_textblock#��[ 5      �� 5   
   $��Пароль��6   $��2��6"   $��0��6/   #��[ 5      �� 5    
   -   5         ��Name$ TbLogin��5       $��1��6#   $��1��60     base_textbox#��[ 5!      G� 5"   
   -   5         G�Name$ TbPass��5"      $��2��6&   $��1��63     base_password#��[ 6C   �� 5#   
   $��3��6   $��0��6"   $��2��5$       �� Orientation$ 
Horizontal=�5%      $��Center=�5&      .+�5'      �� -   5         ��Name$ BtnOk��5'      $
��ОК��6$     base_button#��[ 61   6V   �� 5(      -   5        $ 	BtnCancel��5(      $��Отмена��6(   #��[ 69   6^   5)      5*      5+      