   M S B A M L   `   `   `  ����U  Qhalcondotnet, Version=20.11.1.2, Culture=neutral, PublicKeyToken=4973bed59ddbf2b8A0clr-namespace:HalconDotNet;assembly=halcondotnetHalconDotNet  5   	   L HHALCONMvvmExample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:HALCONMvvmExample.ViewHALCONMvvmExample.View 5   	   '   !HALCONMvvmExample.View.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   IHalconDotNet0clr-namespace:HalconDotNet;assembly=halcondotnet     5   	   6local$clr-namespace:HALCONMvvmExample.View     5   	     � Title$  
MainWindow��5	   	   $	��450��6   $
��1024��6)    � DataContext 5
   	   �� *Main+   Locator �� Source# [  .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $	��150 �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   )   #HalconDotNet.HSmartWindowControlWPF  -   5          Name$
 HSM0��5   >   $��0��6.   $	��200��6L      HDisplayCurrentObject 6[   �� *SingleImage+�� 5   
   $��0��6   $��1��6%   $��5q�62   .+�5      �� $��Display Single Image��69   ��6   �� *LoadSingleImage+69   �� 5      $
��Wrap=�6   $��0,5,0,0q�6,   JHYou can use the HDisplayCurrentObject property to bind a single HObject.5      5        5   
   -   5        $
 HSM1��5   >   $��1��6.   $	��200��6L   .@�5      *   $HalconDotNet.HIconicDisplayObjectWPF     IconicObject 63   �� *	ImageItem+  5         HDraw$
 fill��6v    63   �� *
RegionItem+	   HColor	 6W   �� *RegionColor+6v   +   %HalconDotNet.HMessageDisplayObjectWPF  5       
   HMessageText
 64   �� *MessageText+5!      �� 5"   
   $��1��6   $��1��6%   $��5q�62   .+�5#      �  5$      �� 5%      �� 6+   �� 5&       5(      �� 5)      �� 5*      .+�5,      �� $��0��6   $��Display Image��6K   ��6)   �� *LoadImageItem+6K   �� 5-      $��1��6   $��Display Region��6L   ��6)   �� *LoadRegionItem+6L   �� 5.      $��2��6   $��Display Message��6M   ��6)   �� *LoadMessageText+6M   �� 5/      $��3��6   $��1��6-   $��	Clear All��6Y   ��6:   �� *
ClearItems+6Y   50      �� 51      $
��Wrap=�6   $��0,5,0,0q�6,   ��You can add HIconicDisplayObjectWPFs and HMessageDisplayObjectWPFs to the HSmartWindowControlWPF Items in XAML or code behind, and bind their properties to HObject, etc. properties in your ViewModel.54      55        56   
   $��2��6.   $	��200��6�   ��6>   �� *MultipleObjects+   HColor 6f   ��  �� Path$
 Text� �� ElementName$ HSM2ColorBox��58      �� 59   
   $��2��6   $��1��6%   $��5q�62   .+�5:      �� -   5         ��Name$
 HSM2��5:   5   $��Display Image + Region��6C   ��6   �� *LoadObjects+6C   �� 5;      -   5         ��Name$ HSM2ColorBox��5;       �� SelectedIndex$ 0��6-   $��0,5,0,0q�6?   .@�5<      �� $	��red��6   �� 5=      $��green��6   �� 5>      $��white��6   5?      �� 5@      $
��Wrap=�6   $��0,5,0,0q�6,   /-The Button displays an image and two regions.�� 5A   @   �� 6L   B@The color of the first region can be controlled by the ComboBox.�� 5B   S   �� 6_   @>The color of the second region is controlled by the ViewModel.5D      5E      5F      5G      