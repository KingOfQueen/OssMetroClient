   M S B A M L   `   `   `  ����  OssClientMetro'    !OssClientMetro.Views.DownloadView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      U QCaliburn.Micro, Version=1.3.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  5      $
��Auto��5      $
��Auto��6     � 	Resources  5       �� MergedDictionaries �� Source�� 5
   
    5      �� 5      $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?�6%   �� 5      $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?�6%   �� 5      $e _pack://application:,,,/MahApps.Metro;component/Styles/Controls.AnimatedSingleRowTabControl.xaml?�6%   �� 5      $I Cpack://application:,,,/MahApps.Metro.Resources;component/Icons.xaml?�6%   %    5      .��5      � -   5        $
��root��5        5   
      FileNameColumnDataTemplate �� Orientation  PercentColumnDataTemplate � IsIndeterminate � Value  SpeedColumnDataTemplate  LongSizeToString �� 	Converter  OperateColumnDataTemplate � ToolTip  MyMetroButton  Caliburn.Micro.Message   Attach	 �� 
TargetType
 �� Property  
BlackBrush �� Value 	 Grey3 �� 
TargetType ��Name J� SnapsToDevicePixels
 � Tag �� ElementName � ContextMenu  Caliburn.Micro.Action   TargetWithoutContext �� Path �� RelativeSource ��Name �� Triggers P� Value P� Property �� 
TargetName  AccentColorBrush 	 AccentColor3 �� Color ]� 
Conditions �� Property �� Value 
 AccentColor2��%  &	        &	 �     &	     7  &	 �    7  7  '�� �    �� 5      .R�5      �� $ 
Horizontal=�6   .+�5      �� $��15��67   $��15��6B   $��5,0,5,0q�6N   ��6   �� *	IconUri+6N   �� 5      ��6    �� *displayName+5      5      �� 5      .R�5      �� $ 
Horizontal=�6   .+�5      �  .  $��10��6U   $	��130��6a   $��Center=�6m    6"   �� *	Percent+6�   �� 5       $��Center=�6:   $��5,0,0,0q�6U   ��6    �� *	Percent+6h   �� 5!      $�� %��6    $��Center=�6+   6H   5"      5%      �� 5'      .R�5(      �� $ 
Horizontal=�6   .+�5)      �� ��6    �� *Speed+8   �� 5*      $��/S��6    5+      5,      �� 5.      .R�5/      �� $ 
Horizontal=�6   .+�51      �� $��24��6   $ 打开所在文件夹��6)   8��  $5 /[Event Click] = [openLocalFolder($dataContext)]��6c   .��52      �� $��../Images/openfolder.png��6    $��16��6B   $��5,0,5,0q�6N   6a   53      �� 54      $��24��6   $ 删除��6*   8�� $3 -[Event Click] = [deleteOperate($dataContext)]��6_   .��55      �� $��../Images/delete2.png��6    $��16��6?   $��5,0,5,0q�6K   6^   56      57      5:      �� 5<      "	 ��6   .�5=      �� 
 � ��# �  6/   �� 5>      
 � ��$ Stretch=�6?   �� 5?      
 � ��$ Center=�6=   �� 5@      
 � ��$ 25��6.   �� 5A      
 � ��$ 0,0,0,0q�6+   �� 5B      
 � ��# �  67   �� 5C      
 � �� 5D      �� 5E      " ��6*   .R�5F      �� $ Bd��6%   #��z��61    . ._�5G   "   � 
������  6'   �� *DataContext+$
 root�� 5H   &   �� 5I   *    67   �� $ PlacementTarget.Tag� �� *Self+.@�5J   .   m� $��打开本地文件��67   $) #[Event Click] = [openLocalFolder()]��6H   6�   m� 5K   .   $��删除��67   $' ![Event Click] = [deleteOperate()]��6D   6}   5L   +   .+�5N   &   �� #��z��6;   �� 5O   &   $ contentPresenter��67   $y�	Collapsed=�6Q   5P   #   5Q       5R      P� 5S   "    6O   Z�  �@��ColumnCollection. �5T   &   �� $ contentPresenter��6-   
 � y�$ Visible=�6a   5U   #   P� 5V   "    �@��
IsSelected . . �5W   &   �� $ Bd��6-   
 � �� �	#FF143c656T   �� 5X   &   $ Bd��6C   # �  6S   
 � ��6�   5Y   #   P� 5Z   "    �@J�IsMouseOver . . �5[   &   �� $ Bd��6-   
 � �� 5\   *   �� 5]   .   # � 	 6>   5_   '   5a   #   ]� 5b   "    5c   &   �� 5d   *    �@��
IsSelected . 6J   �� 5e   *    �@��IsSelectionActive . 6Z   ./�5g   &   �� $ Bd��6C   
 � �� 5h   *   �� 5i   .   # � 
 6>   5k   '   �� 5l   &   $ Bd��6D   
 � �� �	#FF143c656T   5m   #   ]� 5n   "    5o   &   �� 5p   *    �@��
IsSelected . 6J   �� 5q   *    �@��IsSelectionActive .  6Z   ./�5s   &   �� $ Bd��6C   
 � �� 5t   *   �� 5u   .   # � 
 6>   5w   '   �� 5x   &   $ Bd��6D   
 � �� �	#FF143c656T   5y   #   5{      5}      5~      5�      .+�5�   
   �� -   5         ��Name$ 
ObjectList��5�   �    �� SelectionMode$ Single=�6   $	��550��6�   $	��Top=�6�   $��0,0,10,0q�6�   y�6+   �� *DownloadVis+  BoolToVisibleConverter# [   �� SelectedIndex  6�   �� *selectedIndex+! �� View! 5�      �� 5�      .P�5�      �� $��名称��6%   " �� CellTemplate#" [  61   # �� Width$	# 200��6l   �� 5�      $��大小��6%   $# 80��6�   $ �� DisplayMemberBinding$ 63   �� *Size+# [ 6�   �� 5�      $��进度��6%   #" [ 62   $	# 180��6l   �� 5�      $��速度��6%   #" [ 62   $# 80��6j   �� 5�      $��操作��6%   #" [ 62   $	# 100��6l   5�      5�      �� 5�   
   $ Single=�6   $	��550��6�   $	��Top=�6�   $��0,0,10,0q�6�   y�6+   �� *CompleteVis+# [ ��6�   �� *compeletedListModel+  6�   �� *selectedIndex+! 5�      �� 5�      .P�5�      �� $��名称��6%   #" [  61   $	# 230��6l   �� 5�      $��大小��6%   $# 80��6�   $ 63   �� *Size+# [ 6�   �� 5�      $��状态��6%   $# 80��6d   $ 63   �� *compeleteStatus+6d   �� 5�      $��完成时间��6%   $	# 150��6c   $ 63   �� *CompleteTime+6c   �� 5�      $��操作��6%   #" [ 62   $	# 100��6l   5�      5�      5�      5�      