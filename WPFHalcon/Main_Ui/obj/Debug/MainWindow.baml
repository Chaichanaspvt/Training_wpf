   M S B A M L   `   `   `  ����B  >Main_Ui, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:Main_UiMain_Ui  5   	   U Qhalcondotnet, Version=20.11.1.2, Culture=neutral, PublicKeyToken=4973bed59ddbf2b8A0clr-namespace:HalconDotNet;assembly=halcondotnetHalconDotNet 5   	   I ELibrary_widget, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG4clr-namespace:Library_widget;assembly=Library_widgetLibrary_widget 6O       Main_Ui.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Main_Ui     5   	   IHalconDotNet0clr-namespace:HalconDotNet;assembly=halcondotnet     5   	   OLibrary_widget4clr-namespace:Library_widget;assembly=Library_widget     6O   -   5          �Name$  MainUi��5   �    � Title$ 
MainWindow��5
   	   $	��600��6   $	��800��6)   .��5      �  � RowDefinitions 5   
   �� 5      $��10 �6   �� 5      $	��400 �6   �� 5      $��* �6   �� 5      $��20 �6    � ColumnDefinitions 5   
   �� 5      $��20 �6   �� 5      $	��400 �6   �� 5      �� 5      $��208.8 �6   �� 5      $��20 �6   .+�5   
   )  #HalconDotNet.HSmartWindowControlWPF  -   5          Name$
 Disp��5   .   $��1��6<   $
��Left=�6L   $	��380��6g   $��10,10.4,0,0q�6t   $��1��6�   $	��Top=�6�   $	��380��6�   A� 5   
   -   5         A�Name$ Light��5      $��2��6!   ���	#FFA0A0A0$
��Left=�6B   $��77��6]   $��
23,0.4,0,0q�6i   $��1��6}   
���   �$	��Top=�6�   $��99��6�   �� 5   
   $��Camera��6   $��2��6!   $��Center=�61   $��42��6N   $��39,95.4,22.8,262.6q�6Z   $��1��6v   $��Center=�6�   $��83��6�   $��18(�6�    J� RenderTransformOrigin$ 0.494,1.038&�6�   �� 5   
   -   5         ��Name$ lb_cycletime��5      $=��7Cycle time ............................................��6&   $��1��6h   $
��Left=�6x   $��
26,0.4,0,0q�6�   $��2��6�   $	��Top=�6�   $��60��6�   $	��364��6�   $��24(�6�      Main_Ui.ToggleButton  5    
   -   5          Name$ Btn_Run��5          Main_Ui.ToggleButton	   MouseLeftButtonDown$(	 "Btn_open_Image_MouseLeftButtonDown��6�   $��3��6/   $
��Left=�6?   $��95��6Z   $��36,0,0,0q�6f   $	��Top=�6x   $	��135��6�   $ 0.408,0.292&�6�   $��1��6�   �� 5!   
   $	��Run��6   $��3��6   $��Center=�6.   $��42��6K   $��80,95,82,263q�6W   $��1��6m   $��Center=�6z   $��47��6�   $��18(�6�   5(      5)      