   M S B A M L   `   `   `  ���� B  >CS_Mgmt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:CS_Mgmt.Views.PatternViewsCS_Mgmt.Views.PatternViews  1    +CS_Mgmt.Views.PatternViews.ViewPatternFloss   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     :local(clr-namespace:CS_Mgmt.Views.PatternViews     -     N� 
Background
  ����� N� Title$ ViewPatternFloss��.��� .+��� -    ��Name$ ViewPatternFlossLabel��$��View Pattern Floss��$
��Left=�$��	37,21,0,0q�$	��Top=�$��26(��� -    ��Name$ ReturnToDash��$��Return to Dashboard��$
��Left=�$��
37,420,0,0q�$	��Top=��� -    ��Name$ PatternColorsStack��$��110,71,140,50q�.+�&   System.Windows.Controls.DataGrid  -     Name$ PatternColorsDG��   SelectionMode$ Single=�$	��329��$	��550��   AutoGenerateColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   Header$	 ID��1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *	FlossID+  $	 Color��
 �� *Color+  $	 Skeins Needed��
 �� *SkeinsNeeded+  $	 Skeins Owned��
 �� *SkeinsOwned+�� -   $ SaveToExcel��$��Save to Excel Spreadsheet��$
��Left=�$��176,420,0,0q�$	��Top=�