   M S B A M L   `   `   `  ���� B  >CS_Mgmt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:CS_Mgmt.Views.PatternViewsCS_Mgmt.Views.PatternViews  +    %CS_Mgmt.Views.PatternViews.AddPattern   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     :local(clr-namespace:CS_Mgmt.Views.PatternViews     -     N� 
Background
  ����� N� Title$ 
AddPattern��.��� .+��� -    ��Name$ AddPatternLabel��$��Add Pattern��$
��Left=�$��	37,21,0,0q�$	��Top=�$��26(��� -   $ PatternInfoLabel��$��Pattern Info��$
��Left=�$��	82,78,0,0q�$	��Top=�$��22(��� -    ��Name$
 Save��$
��Save��$
��Left=�$��132,383,0,0q�$	��Top=��� -   $ Cancel��$��Cancel��$
��Left=�$��191,383,0,0q�$	��Top=��� -   $ PatternColorsLabel��$��Pattern Colors��$
��Left=�$��
479,78,0,0q�$	��Top=�$��22(��� -   $ 	NameLabel��$
��Name��$
��Left=�$��118,125,0,0q�$	��Top=�$��14(��� -    ��Name$ NameTB��$
��Left=�$��170,129,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��170��$��14(��� -	   $ StatusLabel��$��Status��$
��Left=�$��118,161,0,0q�$	��Top=�$��14(��� -
    ��Name$ StatusCB��$
��Left=�$��170,165,0,0q�$	��Top=�$	��120�� �� IsDropDownOpen .  �� -   $ DimensionsLabel��$��
Dimensions��$
��Left=�$��
83,197,0,0q�$	��Top=�$��14(��� -   $ DimensionsTB��$
��Left=�$��170,201,0,0q�$
 Wrap=�$	��Top=�$	��120��$��14(��� -   $ FabricColorLabel��$��Fabric Color��$
��Left=�$��
81,233,0,0q�$	��Top=�$��14(��� -   $ FabricColorTB��$
��Left=�$��170,237,0,0q�$
 Wrap=�$	��Top=�$	��120��$��14(��� -   $ CreatorLabel��$��Creator��$
��Left=�$��109,269,0,0q�$	��Top=�$��14(��� -   $ 	CreatorTB��$
��Left=�$��170,273,0,0q�$
 Wrap=�$	��Top=�$	��170��$��14(��� -   $ SourceLabel��$��Source��$
��Left=�$��109,305,0,0q�$	��Top=�$��14(��� -   $ SourceTB��$
��Left=�$��170,309,0,0q�$
 Wrap=�$	��Top=�$	��170��$��14(��� -   $ StorageLocationLabel��$��Storage Location��$
��Left=�$��
47,341,0,0q�$	��Top=�$��14(��� -   $ StorageLocationTB��$
��Left=�$��170,345,0,0q�$
 Wrap=�$	��Top=�$	��170��$��14(��� -   $ 
ColorLabel��$��Color��$
��Left=�$��486,279,0,0q�$	��Top=�$��14(��� -   $ FlossColorCB��$
��Left=�$��534,283,0,0q�$	��Top=�$	��120�� .  �� -   $ SkeinsNeededLabel��$��Skeins Needed��$
��Left=�$��427,316,0,0q�$	��Top=�$��14(��� -   $ SkeinsCB��$
��Left=�$��534,319,0,0q�$	��Top=�$	��120�� .  �� -   $	 Add��$	��Add��$
��Left=�$��508,356,0,0q�$	��Top=��� -   $ Delete��$��Delete��$
��Left=�$��556,356,0,0q�$	��Top=��� -    ��Name$ PatternColorsStack��$��461,123,149,186q�.+�&   System.Windows.Controls.DataGrid  -   	  Name$	 PatternColorsDG��$	��142��
   SelectionMode$
 Single=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	FlossID+  $ Color�� �� *Color+  $ Skeins Needed�� �� *SkeinsNeeded+