   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$  
MainWindow��5   	   $	��800��6   $	��550��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5       � ColumnDefinitions 5   
   �� 5      .+�5   
   �� $��0��6   $"��/images/Background/Track.png��6    $
��Fill=�5      $	��500��5      $	��500��5      $��30 40 q�5      $��Center=�5      �� 5   
   -   5         ��Name$ 
RaceCanvas��5      $��0��6   $	��500��62   $	��400��6>   �� 5!   
    �� Orientation$ 
Horizontal=�6   $��0��6.   $��Bottom=�6;   $��0q�6V   .+�5$      �� $��Number of horses:��6   $��Center=�5%      $
��20 0q�5&      �� 5(      -   5         ��Name$ horseCountComboBox��5(      $	��120��5*      $��30��5+       �� SelectedIndex$ 1��5,      .@�5.      �� $��2��6   �� 5/      $��3��6   �� 50      $��4��6   �� 51      $��5��6   �� 52      $��6��6   53      �� 54      -   5        $��Start��55      $��90��56      $��30��6   $�� 40 0q�58      5=      &   System.Windows.Controls.DataGrid  5C   
   -   5          Name$ HorseInfoGrid��5C         AutoGenerateColumns .  $��1��5F      	   
IsReadOnly	 . 
   CanUserAddRows
 .  $	��112��5I      $	��400��5J      $
��0 10q�5K      $	��Top=�5L      $��Center=�5M      ��5D      �� *Horses+   Columns 5O      0  *System.Windows.Controls.DataGridTextColumn  5P      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *Name+6L   4  .System.Windows.Controls.DataGridTemplateColumn  5Q      $ 	ColorName��6)      CellTemplate 5R      �� 5S      .R�5T      �� $��20��6(   $��20��63   
���   ���6?   �� *
ColorBrush+6[   5U      5W        5X      $ Posicion��6%   $ * 6T    67   �� *
Position+6T     5Y      $
 Time��6%   $ * 6S    63   �� *TimeRunning+6S   5\      5c      5d      