   M S B A M L   `   `   `  ����I  EOssClientMetro, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:SortListView.ExtentionSortListView.Extention  5      H DMahApps.Metro, Version=0.9.0.0, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5	      %    OssClientMetro.Views.ObjectView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      U QCaliburn.Micro, Version=1.3.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  5      .ext$clr-namespace:SortListView.Extention  5      JControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  5	      $
��Auto��5      $
��Auto��6     � 	Resources  5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?�6%   �� 5      $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?�6%   �� 5      $e _pack://application:,,,/MahApps.Metro;component/Styles/Controls.AnimatedSingleRowTabControl.xaml?�6%   �� 5      $I Cpack://application:,,,/MahApps.Metro.Resources;component/Icons.xaml?�6%   %    5      .��5      � -   5        $
��root��5        5   
    �� 
TargetType �� Property �� Value   test  Caliburn.Micro.Message  
BlackBrush 	 Grey3 �� 
TargetType ��Name J� SnapsToDevicePixels
	 � Tag
 �� ElementName � ContextMenu  Caliburn.Micro.Bind   Model �� Path �� RelativeSource  Caliburn.Micro.Action   TargetWithoutContext   Attach  BoolToVisibleConverter �� 	Converter ��Name �� Triggers P� Value P� Property �� 
TargetName  AccentColorBrush  AccentColor3 �� Color ]� 
Conditions �� Property �� Value  AccentColor2  FileNameColumnDataTemplate �� Orientation   ModifyTimeColumnDataTemplate��%�
  '��       &	  Z     7  7  7  &	 	    &	 �	    7  �� 5      " ��6   .�5      ��  � ��$ Microsoft YaHei)�6/   6I   5      �� 5      " ��6!   .�5      ��  �@ Attach$6 0[Event PreviewMouseDoubleClick] = [OpenFolder()]��67   �� 5        � ��# �  6/   �� 5!       � ��$ 25��6.   �� 5"       � ��$ 30��6+   �� 5#       � ��$ 0,0,0,0q�6+   �� 5$       � ��# �  67   �� 5&       � �� 5'      �� 5(      " ��6*   .R�5)      �� $ Bd��6%   #��z��61    . ._�5*   "   � 
������ 	 6'   �� *DataContext+$

 root�� 5+   &   �� 5,   *    66   �� $ PlacementTarget.Tag� �� *Self+ 6�   �� $ PlacementTarget.Tag� �� *Self+.@�5-   .   m� $��打开��67   $$ [Event Click] = [OpenFolder()]��6F   y�6{   �� *
IsFolder+8   6�   m� 5/   .   $��下载��67   $" [Event Click] = [download()]��6D   6x   m� 51   .   $��删除��67   $  [Event Click] = [delete()]��6�   y�6D   �� *IsDeleteEnable+8  6�   m� 52   .   $��打开所在文件夹��67   $, &[Event Click] = [searchOpenLoaction()]��6I   y�6�   �� *IsSearchView+8  6�   53   +   .+�55   &   �� #��z��6;   �� 56   &   $ contentPresenter��67   $y�	Collapsed=�6Q   $��Center=�6h   57   #   58       59      P� 5:   "    6O   Z�  �@��ColumnCollection. �5;   &   �� $ contentPresenter��6-    � y�$ Visible=�6a   5<   #   P� 5=   "    �@��
IsSelected . . �5>   &   �� $ Bd��6-    � �� �	#FF143c656T   �� 5?   &   $ Bd��6C   # �  6S    � ��6�   5@   #   P� 5A   "    �@J�IsMouseOver . . �5B   &   �� $ Bd��6-    � �� 5C   *   �� 5D   .   # �  6>   5F   '   5H   #   ]� 5I   "    5J   &   �� 5K   *    �@��
IsSelected . 6J   �� 5L   *    �@��IsSelectionActive . 6Z   ./�5N   &   �� $ Bd��6C    � �� 5O   *   �� 5P   .   # �  6>   5R   '   �� 5S   &   $ Bd��6D    � �� �	#FF143c656T   5T   #   ]� 5U   "    5V   &   �� 5W   *    �@��
IsSelected . 6J   �� 5X   *    �@��IsSelectionActive .  6Z   ./�5Z   &   �� $ Bd��6C    � �� 5[   *   �� 5\   .   # �  6>   5^   '   �� 5_   &   $ Bd��6D    � �� �	#FF143c656T   5`   #   5b      5d      5e      �� 5g      .R�5h      �� $ 
Horizontal=�6   .+�5i      �� $��15��67   $��15��6B   $��5,0,5,0q�6N   ��6   �� *	IconUri+6N   �� 5j      ��6    �� *displayName+5k      5m      �� 5o      .R�5p      �� $ 
Horizontal=�6   y�66   �� *modifyTimeVisible+8   .+�5q      �� ��6    �� *
ModifyTime+5r      5t      5u       � RowDefinitions 5v   
   �� 5w      $��50 �6   6*   �� 5x      $��40 �6   6*   �� 5y      $��* �6   6)   .+�5{   
   �� $��0��6   $��0,0,0,1q�6   #���  68   6k   �� 5}   
   -   5         ��Name$ goback��5}       � ToolTip$ 后退��6$   $��0q�61   
������  	 MyMetroButton#��[	 6_   $��35��5~      $��35��5      $��Center=�5�      $
��Left=�6:   $��10,0,0,0q�6U   $��Normal"�6�   {�6h   �� *history.CanGoBack+.��5�      �� $��../Images/left.png��6   $��-6q�60   6>   5�      �� 5�   
   -   5        $ gofoward��5�      $ 前进��6#   $��0q�60   
������ #��[	 6]   $��35��5�      $��35��5�      $
��Left=�6*   $��45,0,0,0q�6F   $��Center=�6X    J� RenderTransformOrigin$ 0.487,-0.006&�6�   {�6s   �� *history.CanGoForward+.��5�   
   �� $��../Images/right.png��6   $��-6q�6-   6;   5�      �� 5�   
   -   5        $ refresh��5�      #��[	 6"   $ 刷新��6v   $��0q�6�   
������ $
��Left=�6�   $��35��6�   $��85,0,0,0q�6�   $��Center=�6�   $��35��6  {�6I   �� *IsCreateFolderEnabled+.��5�      �� $��../Images/refresh.png��6   $��-5q�64   6C   5�      �� 5�   
     �� 
IsReadOnly  . $��Center=�6%   $
��Left=�6h   $��24��6�   $��	134,0,0,0q�6�   ! �� TextWrapping$
! Wrap=�6�   $��Center=�6�   $	��371��6�   ��6G   �� *history.NowPath+6�   �� 5�   
   -   5        $��searchTextBox��5�   [   $��Center=�68   *  $MahApps.Metro.Controls.TextboxHelper"   	Watermark$" 搜索��6q   $
��Left=�6�   $��24��6�   $��	515,0,0,0q�6�   $
! Wrap=�6�   $��Center=�6�   $	��162��6   $= 7[Event KeyDown] = [Action keydown($source, $eventArgs)]��6  {�6   �� *IsSearchEnabled+6  �� 5�   
   #��[	 67   $��0q�6`   
������ $@ :[Event Click] = [Action searchOperate(searchTextBox.Text)]��6�   $
��Left=�6�   $��	653,0,0,0q�6�   $��Center=�6  $��22��6)  $��22��64  {�6   �� *IsSearchEnabled+.��5�      �� $��../Images/search.png��6   66   5�      �� 5�   
   $��1��6   $ 
Horizontal=�6#   .+�5�      �� -   5        $ createFolderOperate��5�   o   #��[	 6   $
��Left=�6�   $��25��6�   $��20,0,0,0q�6�   $��Center=�6�   $	��120��6�   {�6B   �� *IsCreateFolderEnabled+.��5�      �� $ 
Horizontal=�6   .+�5�      �� $��../Images/add_folder.png��6   $��5,2,5,2q�6>   6Q   �� 5�      $��创建文件夹��6    $��16(�6-   $��Microsoft YaHei)�6=   6]   5�      5�      �� 5�      -   5        $ download��5�   �   #��[	 6<   $��0q�6c   
������ $��1��6�   $
��Left=�6�   $��24��6�   $��20,0,0,0q�6�   $��Center=�6�   $��90��6  $
��Left=�6  {�6   �� *IsDownloadEnable+.��5�      �� $ 
Horizontal=�6   .+�5�      �� $��../Images/download.png��6   $��-5,-12,-5, -12q�6<   6V   �� 5�      $��下载��6    $��16(�6*   $��Microsoft YaHei)�68   6W   5�      5�      �� 5�      -   5        # �� 	IsEnabled# .  #��[	 5�   |   $��1��6�   $
��Left=�6�   $��24��6�   $��20,0,0,0q�6�   $��Center=�6�   $	��100��6  {�6   �� *IsCreateFolderEnabled+.��5�      �� $ 
Horizontal=�6   .+�5�      �� $��../Images/upload.png��6   $��-5,-12,-5, -12q�6:   6T   �� 5�      $��上传��6    $��16(�6*   $��Microsoft YaHei)�68   6W   �� 5�      $��../Images/down.png��6   $��-5,-6,-5, -6q�68   6P   5�       5�      �� 5�      $��Microsoft YaHei)�6"   .@�5�      m� $��上传文件��6#   $+ %[Event Click] = [uploadFileOperate()]��62   $��14(�6m   6}   m� 5�      $��上传文件夹��6#   $- '[Event Click] = [uploadFolderOperate()]��63   $��14(�6p   6�   m� 5�      $��压缩并上传文件夹��6#   $0 *[Event Click] = [uploadFolderZipOperate()]��67   $��14(�6w   6�   5�      5�      �� 5�      -	   5        $ delete��5�   c   #��[	 6;   $
��Left=�6s   $��24��6�   $��20,0,0,0q�6�   $��Center=�6�   $��84��6�   {�6   �� *IsDeleteEnable+.��5�      �� $ 
Horizontal=�6   .+�5�      �� $��../Images/delete.png��6   $��-5,-12,-5,-12q�6:   6S   �� 5�      $��删除��6    $��16(�6*   6;   5�      5�      5�      �� 5�   
   $��2��6   .+�5�   
   �� -
   5        $ ��Name$$ 
objectList��5�   Y   % �� SelectionMode$% Single=�6   #���   6+   $	��455��6�   $	��Top=�6�   & J� 	AllowDrop& . $3 -[Event Drop] = [Action DragEnter($eventArgs)]��5�      $��0,0,10,0q�6W   -   'SortListView.Extention.ListViewBehavior'   
HeaderSort' . ( �� SelectedIndex( 5�   m   �� *selectedIndex+) �� View) 5�      �� 5�      .P�5�      �� $��名称��6%   * �� CellTemplate#* [ 61   + �� Width$	+ 300��6l   ,   	SortField$, displayName��6x   �� 5�      $��大小��6%   $	+ 100��6�   $
, Size��6�   - �� DisplayMemberBinding- 61   �� *Size+ 
 LongSizeToString# [
 6�   �� 5�      $��修改时间��6%   #* [ 63   $	+ 200��6p   $, 
ModifyTime��6|   5�      5�      .  (MahApps.Metro.Controls.ProgressIndicator  5�      .   ProgressColour#. [ 6_   $��6��5�   5   $	��600��5�   5   / � DataContext/ 5�   )   �� *DataContext+$

 root��y�5�   A   �� *ProgressVisible+# [ 5�      5�      5�      