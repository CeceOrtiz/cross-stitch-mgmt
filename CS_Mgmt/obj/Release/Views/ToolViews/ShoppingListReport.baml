   M S B A M L   `   `   `  ���� B  >CS_Mgmt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:CS_Mgmt.Views.ToolViewsCS_Mgmt.Views.ToolViews  0    *CS_Mgmt.Views.ToolViews.ShoppingListReport   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     7local%clr-namespace:CS_Mgmt.Views.ToolViews     -     N� 
Background
  ����� N� Title$ ShoppingListReport��.��� .+��� -    ��Name$ ShoppingListLabel��$��Shopping List��$
��Left=�$��	37,21,0,0q�$	��Top=�$��26(��� -    ��Name$ ReturnToDash��$��Return to Dashboard��$
��Left=�$��
37,420,0,0q�$	��Top=��� -   $ SaveToExcel��$��Save to Excel Spreadsheet��$
��Left=�$��176,420,0,0q�$	��Top=��� -   $ ItemTypeLabel��$��	Item Type��$
��Left=�$��
484,79,0,0q�$	��Top=�$��14(��� -    ��Name$ 
ItemTypeCB��$
��Left=�$��
560,83,0,0q�$	��Top=�$	��120�� �� IsDropDownOpen .  .@��� $��Pattern���� $��Floss���� $��Fabric���� $��
Other Item���� -    ��Name$ 
ItemsStack��$��37,83,451,57q�.+�&   System.Windows.Controls.DataGrid  -     Name$ ItemsDG��$	��310��   SelectionMode$ Single=�	   AutoGenerateColumns	 .  
   Columns
 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Type of Item��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Item+  $
 Name�� �� *Name+  $ Quantity�� �� *
Quantity+  $ Where to Buy�� �� *Store+�� -	   $ 
NameLabel1��$
��Name��$
��Left=�$��467,138,0,0q�$	��Top=�$��14(��� -
   $ 
ItemLabel1��$
��Item��$
��Left=�$��476,138,0,0q�$	��Top=�$��14(��� -   $ 
TypeLabel1��$
��Type��$
��Left=�$��476,138,0,0q�$	��Top=�$��14(��� -   $ ColorLabel1��$��Color��$
��Left=�$��471,138,0,0q�$	��Top=�$��14(��� -    ��Name$ FirstTB��$
��Left=�$��519,142,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��170��$��14(��� -   $ StoreLabel2��$��Store��$
��Left=�$��472,178,0,0q�$	��Top=�$��14(��� -   $ QuantityLabel2��$��Quantity��$
��Left=�$��451,178,0,0q�$	��Top=�$��14(��� -   $ ColorLabel2��$��Color��$
��Left=�$��474,178,0,0q�$	��Top=�$��14(��� -   $ SecondTB��$
��Left=�$��519,182,0,0q�$
 Wrap=�$	��Top=�$	��170��$��14(��� -   $ StoreLabel3��$��Store��$
��Left=�$��472,218,0,0q�$	��Top=�$��14(��� -   $ CountLabel3��$��Count��$
��Left=�$��467,218,0,0q�$	��Top=�$��14(��� -   $ ThirdTB��$
��Left=�$��519,222,0,0q�$
 Wrap=�$	��Top=�$	��170��$��14(��� -   $ QuantityLabel4��$��Quantity��$
��Left=�$��451,258,0,0q�$	��Top=�$��14(��� -   $ FourthTB��$
��Left=�$��519,262,0,0q�$
 Wrap=�$	��Top=�$	��170��$��14(��� -   $ StoreLabel5��$��Store��$
��Left=�$��472,298,0,0q�$	��Top=�$��14(��� -   $ FifthTB��$
��Left=�$��519,302,0,0q�$
 Wrap=�$	��Top=�$	��170��$��14(��� -   $ FlossColorCB��$
��Left=�$��519,141,0,0q�$	��Top=�$	��170�� .  �� -   $ AddItem��$��Add Item��$
��Left=�$��567,345,0,0q�$	��Top=��� -   $ 
RemoveItem��$��Remove Selected Item��$
��Left=�$��533,378,0,0q�$	��Top=�