   M S B A M L   `   `   `  ���� I  EOssClientMetro, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:SortListView.ExtentionSortListView.Extention  H DMahApps.Metro, Version=0.9.0.0, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls %    OssClientMetro.Views.ObjectView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     U QCaliburn.Micro, Version=1.3.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  .ext$clr-namespace:SortListView.Extention  JControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  $
��Auto��$
��Auto��  � 	Resources   �� MergedDictionaries �� Source��  �� $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?��� $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?��� $e _pack://application:,,,/MahApps.Metro;component/Styles/Controls.AnimatedSingleRowTabControl.xaml?��� $I Cpack://application:,,,/MahApps.Metro.Resources;component/Icons.xaml?�%    .��� -   $
��root��   �� 
TargetType �� Property �� Value   test  Caliburn.Micro.Message  
BlackBrush 	 Grey3 �� 
TargetType ��Name J� SnapsToDevicePixels
	 � Tag
 �� ElementName � ContextMenu  Caliburn.Micro.Bind   Model �� Path �� RelativeSource  Caliburn.Micro.Action   TargetWithoutContext   Attach  BoolToVisibleConverter �� 	Converter ��Name �� Triggers P� Value P� Property �� 
TargetName  AccentColorBrush  AccentColor3 �� Color ]� 
Conditions �� Property �� Value  AccentColor2  FileNameColumnDataTemplate �� Orientation   ModifyTimeColumnDataTemplate��%�  '��       &	  0     7  7  7  &	 �    &	 2    7  �� " ��.���  � ��$ Microsoft YaHei)��� " ��.���  �@ Attach$6 0[Event PreviewMouseDoubleClick] = [OpenFolder()]����  � ��# �  ��  � ��$ 25����  � ��$ 30����  � ��$ 0,0,0,0q���  � ��# �  ��  � �� �� " ��.R��� $ Bd��#��z�� . ._�� 
������ 	 �� *DataContext+$

 root�� ��  �� $ PlacementTarget.Tag� �� *Self+ �� $ PlacementTarget.Tag� �� *Self+.@�m� $��打开��$$ [Event Click] = [OpenFolder()]��y��� *
IsFolder+8   m� $��下载��$" [Event Click] = [download()]��m� $��删除��$  [Event Click] = [delete()]��y��� *IsDeleteEnable+8  m� $��打开所在文件夹��$, &[Event Click] = [searchOpenLoaction()]��y��� *IsSearchView+8  .+��� #��z���� $ contentPresenter��$y�	Collapsed=�$��Center=� P�  Z�  �@��ColumnCollection. ��� $ contentPresenter�� � y�$ Visible=�P�  �@��
IsSelected . . ��� $ Bd�� � �� �	#FF143c65�� $ Bd��# �   � ��P�  �@J�IsMouseOver . . ��� $ Bd�� � �� �� # �  ]�  ��  �@��
IsSelected . ��  �@��IsSelectionActive . ./��� $ Bd�� � �� �� # �  �� $ Bd�� � �� �	#FF143c65]�  ��  �@��
IsSelected . ��  �@��IsSelectionActive .  ./��� $ Bd�� � �� �� # �  �� $ Bd�� � �� �	#FF143c65�� .R��� $ 
Horizontal=�.+��� $��15��$��15��$��5,0,5,0q����� *	IconUri+�� ���� *displayName+�� .R��� $ 
Horizontal=�y��� *modifyTimeVisible+8   .+��� ���� *
ModifyTime+ � RowDefinitions �� $��50 ��� $��40 ��� $��* �.+��� $��0��$��0,0,0,1q�#���  �� -    ��Name$ goback�� � ToolTip$ 后退��$��0q�
������  	 MyMetroButton#��[	 $��35��$��35��$��Center=�$
��Left=�$��10,0,0,0q�$��Normal"�{��� *history.CanGoBack+.���� $��../Images/left.png��$��-6q��� -   $ gofoward��$ 前进��$��0q�
������ #��[	 $��35��$��35��$
��Left=�$��45,0,0,0q�$��Center=� J� RenderTransformOrigin$ 0.487,-0.006&�{��� *history.CanGoForward+.���� $��../Images/right.png��$��-6q��� -   $ refresh��#��[	 $ 刷新��$��0q�
������ $
��Left=�$��35��$��85,0,0,0q�$��Center=�$��35��{��� *IsCreateFolderEnabled+.���� $��../Images/refresh.png��$��-5q���   �� 
IsReadOnly  . $��Center=�$
��Left=�$��24��$��	134,0,0,0q�! �� TextWrapping$
! Wrap=�$��Center=�$	��371������ *history.NowPath+�� -   $��searchTextBox��$��Center=�*  $MahApps.Metro.Controls.TextboxHelper"   	Watermark$" 搜索��$
��Left=�$��24��$��	515,0,0,0q�$
! Wrap=�$��Center=�$	��162��$= 7[Event KeyDown] = [Action keydown($source, $eventArgs)]��{��� *IsSearchEnabled+�� #��[	 $��0q�
������ $@ :[Event Click] = [Action searchOperate(searchTextBox.Text)]��$
��Left=�$��	653,0,0,0q�$��Center=�$��22��$��22��{��� *IsSearchEnabled+.���� $��../Images/search.png���� $��1��$ 
Horizontal=�.+��� -   $ createFolderOperate��#��[	 $
��Left=�$��25��$��20,0,0,0q�$��Center=�$	��120��{��� *IsCreateFolderEnabled+.���� $ 
Horizontal=�.+��� $��../Images/add_folder.png��$��5,2,5,2q��� $��创建文件夹��$��16(�$��Microsoft YaHei)��� -   $ download��#��[	 $��0q�
������ $��1��$
��Left=�$��24��$��20,0,0,0q�$��Center=�$��90��$
��Left=�{��� *IsDownloadEnable+.���� $ 
Horizontal=�.+��� $��../Images/download.png��$��-5,-12,-5, -12q��� $��下载��$��16(�$��Microsoft YaHei)��� -   # �� 	IsEnabled# .  #��[	 $��1��$
��Left=�$��24��$��20,0,0,0q�$��Center=�$	��100��{��� *IsCreateFolderEnabled+.���� $ 
Horizontal=�.+��� $��../Images/upload.png��$��-5,-12,-5, -12q��� $��上传��$��16(�$��Microsoft YaHei)��� $��../Images/down.png��$��-5,-6,-5, -6q� �� $��Microsoft YaHei)�.@�m� $��上传文件��$+ %[Event Click] = [uploadFileOperate()]��$��14(�m� $��上传文件夹��$- '[Event Click] = [uploadFolderOperate()]��$��14(�m� $��压缩并上传文件夹��$0 *[Event Click] = [uploadFolderZipOperate()]��$��14(��� -	   $ delete��#��[	 $
��Left=�$��24��$��20,0,0,0q�$��Center=�$��84��{��� *IsDeleteEnable+.���� $ 
Horizontal=�.+��� $��../Images/delete.png��$��-5,-12,-5,-12q��� $��删除��$��16(��� $��2��.+��� -
   $ ��Name$$ 
objectList��% �� SelectionMode$% Single=�#���   $	��455��$	��Top=�& J� 	AllowDrop& . $3 -[Event Drop] = [Action DragEnter($eventArgs)]��$��0,0,10,0q�-   'SortListView.Extention.ListViewBehavior'   
HeaderSort' . ( �� SelectedIndex( �� *selectedIndex+) �� View) �� .P��� $��名称��* �� CellTemplate#* [ + �� Width$	+ 300��,   	SortField$, displayName���� $��大小��$	+ 100��$
, Size��- �� DisplayMemberBinding- �� *Size+ 
 LongSizeToString# [
 �� $��修改时间��#* [ $	+ 200��$, 
ModifyTime��.  (MahApps.Metro.Controls.ProgressIndicator  .   ProgressColour#. [ $��6��$	��600��/ � DataContext/ �� *DataContext+$

 root��y��� *ProgressVisible+# [ 