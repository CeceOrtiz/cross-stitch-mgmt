   M S B A M L   `   `   `  ���� B  >CS_Mgmt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:CS_Mgmt.Views.ToolViewsCS_Mgmt.Views.ToolViews  -    'CS_Mgmt.Views.ToolViews.InventoryReport   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     7local%clr-namespace:CS_Mgmt.Views.ToolViews     -     N� 
Background
  ����� N� Title$ InventoryReport��.��� .+��� -    ��Name$ ViewInventoryLabel��$��View Inventory��$
��Left=�$��	37,21,0,0q�$	��Top=�$��26(��� -    ��Name$ ReturnToDash��$��Return to Dashboard��$
��Left=�$��
37,420,0,0q�$	��Top=��� -   $ SaveToExcel��$��Save to Excel Spreadsheet��$
��Left=�$��176,420,0,0q�$	��Top=��� -    ��Name$ PatternsStack��$��37,130,48,57q�.+�&   System.Windows.Controls.DataGrid  -     Name$ 
PatternsDG��$	��263��   SelectionMode$ Single=�   AutoGenerateColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   Header$
	 Name��1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *Name+  $	 	Stored In��
 �� *StorageLocation+�� -   $ ItemTypeLabel��$��	Item Type��$
��Left=�$��	51,82,0,0q�$	��Top=�$��14(��� -    ��Name$ 
ItemTypeCB��$
��Left=�$��
127,86,0,0q�$	��Top=�$	��120�� �� IsDropDownOpen .  .@��� $��Patterns���� $��Floss���� $��Fabrics���� $��Other Items���� -	   $ 
FlossStack��$��37,130,48,57q�.+�  -
   $ FlossDG��$	��263��$ Single=� .     $	 Standard Name��
 �� *StandardName+  $	 Color��
 �� *Color+  $	 Quantity��
 �� *
Quantity+  $	 	Stored In��
 �� *StorageLocation+�� -   $ FabricsStack��$��37,130,48,57q�.+�  -   $ 	FabricsDG��$	��263��$ Single=� .     $	 Color��
 �� *Color+  $
	 Type��
 �� *Type+  $	 Count��
 �� *Count+  $	 	Stored In��
 �� *StorageLocation+�� -   $ 
ItemsStack��$��37,130,48,57q�.+�  -   $ ItemsDG��$	��263��$ Single=� .     $
	 Item��
 �� *Item+  $	 Quantity��
 �� *
Quantity+  $	 	Stored In��
 �� *StorageLocation+