   M S B A M L   `   `   `  ����   OssClientMetro'    !OssClientMetro.Views.DownloadView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     U QCaliburn.Micro, Version=1.3.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  $
��Auto��$
��Auto��  � 	Resources   �� MergedDictionaries �� Source��  �� $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?��� $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?��� $e _pack://application:,,,/MahApps.Metro;component/Styles/Controls.AnimatedSingleRowTabControl.xaml?��� $I Cpack://application:,,,/MahApps.Metro.Resources;component/Icons.xaml?�%    .��� -   $
��root��     FileNameColumnDataTemplate �� Orientation  PercentColumnDataTemplate � IsIndeterminate � Value  SpeedColumnDataTemplate  LongSizeToString �� 	Converter  OperateColumnDataTemplate � ToolTip  MyMetroButton  Caliburn.Micro.Message   Attach	 �� 
TargetType
 �� Property  
BlackBrush �� Value 	 Grey3 �� 
TargetType ��Name J� SnapsToDevicePixels
 � Tag �� ElementName � ContextMenu  Caliburn.Micro.Action   TargetWithoutContext �� Path �� RelativeSource ��Name �� Triggers P� Value P� Property �� 
TargetName  AccentColorBrush 	 AccentColor3 �� Color ]� 
Conditions �� Property �� Value 
 AccentColor2��%]  &	        &	 y     &	 +    7  &	 w    7  7  '�� �    �� .R��� $ 
Horizontal=�.+��� $��15��$��15��$��5,0,5,0q����� *	IconUri+�� ���� *displayName+�� .R��� $ 
Horizontal=�.+��  .  $��10��$	��130��$��Center=� �� *	Percent+�� $��Center=�$��5,0,0,0q����� *	Percent+�� $�� %��$��Center=��� .R��� $ 
Horizontal=�.+��� ���� *Speed+8   �� $��/S���� .R��� $ 
Horizontal=�.+��� $��24��$ 打开所在文件夹��8��  $5 /[Event Click] = [openLocalFolder($dataContext)]��.���� $��../Images/openfolder.png��$��16��$��5,0,5,0q��� $��24��$ 删除��8�� $3 -[Event Click] = [deleteOperate($dataContext)]��.���� $��../Images/delete2.png��$��16��$��5,0,5,0q��� "	 ��.��� 
 � ��# �  �� 
 � ��$ Stretch=��� 
 � ��$ Center=��� 
 � ��$ 25���� 
 � ��$ 0,0,0,0q��� 
 � ��# �  �� 
 � �� �� " ��.R��� $ Bd��#��z�� . ._�� 
������  �� *DataContext+$
 root�� ��  �� $ PlacementTarget.Tag� �� *Self+.@�m� $��打开本地文件��$) #[Event Click] = [openLocalFolder()]��m� $��删除��$' ![Event Click] = [deleteOperate()]��.+��� #��z���� $ contentPresenter��$y�	Collapsed=� P�  Z�  �@��ColumnCollection. ��� $ contentPresenter��
 � y�$ Visible=�P�  �@��
IsSelected . . ��� $ Bd��
 � �� �	#FF143c65�� $ Bd��# �  
 � ��P�  �@J�IsMouseOver . . ��� $ Bd��
 � �� �� # � 	 ]�  ��  �@��
IsSelected . ��  �@��IsSelectionActive . ./��� $ Bd��
 � �� �� # � 
 �� $ Bd��
 � �� �	#FF143c65]�  ��  �@��
IsSelected . ��  �@��IsSelectionActive .  ./��� $ Bd��
 � �� �� # � 
 �� $ Bd��
 � �� �	#FF143c65.+��� -    ��Name$ 
ObjectList�� �� SelectionMode$ Single=�$	��550��$	��Top=�$��0,0,10,0q�y��� *DownloadVis+  BoolToVisibleConverter# [   �� SelectedIndex  �� *selectedIndex+! �� View! �� .P��� $��名称��" �� CellTemplate#" [  # �� Width$	# 200���� $��大小��$# 80��$ �� DisplayMemberBinding$ �� *Size+# [ �� $��进度��#" [ $	# 180���� $��速度��#" [ $# 80���� $��操作��#" [ $	# 100���� $ Single=�$	��550��$	��Top=�$��0,0,10,0q�y��� *CompleteVis+# [ ���� *compeletedListModel+  �� *selectedIndex+! �� .P��� $��名称��#" [  $	# 230���� $��大小��$# 80��$ �� *Size+# [ �� $��状态��$# 80��$ �� *compeleteStatus+�� $��完成时间��$	# 150��$ �� *CompleteTime+�� $��操作��#" [ $	# 100��