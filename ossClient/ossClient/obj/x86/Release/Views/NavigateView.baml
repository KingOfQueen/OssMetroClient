   M S B A M L   `   `   `  ���� I  EOssClientMetro, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:WPF.Common
WPF.Common  A%clr-namespace:OssClientMetro.ControlsOssClientMetro.Controls  H DMahApps.Metro, Version=0.9.0.0, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls '    !OssClientMetro.Views.NavigateView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     U QCaliburn.Micro, Version=1.3.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  %commonclr-namespace:WPF.Common  4controls%clr-namespace:OssClientMetro.Controls  JControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  -     >�Name$  userControl��$
��Auto��$
��Auto�����	#ffe5e5e5 � 	Resources  �� MergedDictionaries �� Source��  �� $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?��� $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?��� $e _pack://application:,,,/MahApps.Metro;component/Styles/Controls.AnimatedSingleRowTabControl.xaml?��� $I Cpack://application:,,,/MahApps.Metro.Resources;component/Icons.xaml?�%    .��0  *MahApps.Metro.Controls.MetroContentControl  .���� -   $
��root��    ExpanderHeaderFocusVisual �� Property �� Value J� SnapsToDevicePixels �� StrokeDashArray  ExpanderDownHeaderStyle �� 
TargetType	 �� 
TargetType
 �� Padding � ColumnDefinitions F�Name J� RenderTransformOrigin �� ScaleX �� ScaleY �� Angle$   WPF.Common.VisibilityAnimation   AnimationType �� TextAlignment  BoolToVisibleConverter �� 	Converter �� Triggers P� Property P� Value �� 
TargetName  ExpanderStyle1 �� CornerRadius �� Orientation d�Name � FocusVisualStyle d� 	IsChecked �� Mode �� RelativeSource ��Name  ExpanderDownHeaderStyle2  � ToolTip  MyMetroButton  Caliburn.Micro.Message!   Attach" j� Stretch  
appbar_add# !� Visual  ExpanderStyle2��%�  &	        &	 Z     7  &	 �    7   7  &	 Z    7  &	 �
    7   7  �� .���  � �� �� .R��� ._��� $��0q� . 
���   �$��1��$	 1 2R��� " d�.���  � �� �� "	 d�.R��� #
 z��$��0,0,0,0q�._�� 
������  .   �� $��19 ��� $��* ��� $��80 �.+�F� $ arrow��"����@B `� � �D @KL � z `� 	$��Center=� .  ���#666$��2��$��Center=�$ 0.5,0.5&�z�V� .~��� $	 1.5Q�$	 1.5Q��� �� $	 -90Q�T� �� $��2��$	��-20q���Z� ._��� $
 Fade=�$��26(�$ Center=�$��Center=����� *AddTaskNumText+y��� *AddTaskNumVisibility+8   �� $��1��$
��Left=�$��4,0,0,0q���.  . $��Center=� P�  �@d�	IsChecked$
 trueY�. ��� $ arrow�� � ��" ��@B  �D � �D @B � z  �D 	�� $ arrow�� � z� V� .~��� $	 1.5Q�$	 1.5Q��� �� $	 180Q�T� P�  �@J�IsMouseOver . . ��� $ arrow�� � �� �#222P�  � �� . . ��� $ arrow�� � �� �	#FF003366�� " 9�.���  � ��# � ����  � ��
 ���� ��  � ��$ Stretch=���  � ��$ Stretch=���  � ��
 ���� ��  � ��$ 1q���  � �� �� "	 9�.R��� #��z��#��z��#��z��$ 3�� . ._��� $ Vertical=�.+�d� $ 
HeaderSite��$��40��#��z��#��z��#��z��$	��Top=�#��z��#��z��8   #��z��#��z��#��z��#��z��#��z��$��1q�$��0��$��0��#��z��8�� #��z�� �� *
IsExpanded+$ TwoWay=� �� *TemplatedParent+�� $ 
ExpandSite��|�.  #��z��#��z��$y�	Collapsed=�#��z�� P�  �@9�
IsExpanded . . ��� $ 
ExpandSite�� � y�$ Visible=�P�  � {� .  . ��� # � �� � ���� " d�.���  � �� �� "	 d�.R��� #
 z��$��0,0,0,0q�._�� 
������  .   �� $��19 ��� $��* ��� $��25 ��� $��25 ��� $��25 �.+�F� $ arrow��"����@B `� � �D @KL � z `� 	$��Center=� .  ���#666$��2��$��Center=�$ 0.5,0.5&�z�V� .~��� $	 1.5Q�$	 1.5Q��� �� $	 -90Q�T� �� $  刷新��#���  $��2��$(! "[Event Click] = [refreshBuckets()]��.���� $��../Images/refresh.png���� $  新建Bucket��#���  $��3��$,! &[Event Click] = [Action createBucket2]��.���� $��12��$��12����!� $
" Fill=�8#   �� $  删除��#���  $��4��$-! '[Event Click] = [Action deleteBucket()]��{��� *IsDeleteAvialable+.���� $��../Images/delete2.png��$��16��$��5,0,5,0q��� $��1��$
��Left=�$��4,0,0,0q���.  . $��Center=� P�  �@d�	IsChecked$
 trueY�. ��� $ arrow�� � ��" ��@B  �D � �D @B � z  �D 	�� $ arrow�� � z� V� .~��� $	 1.5Q�$	 1.5Q��� �� $	 180Q�T� P�  �@J�IsMouseOver . . ��� $ arrow�� � �� �#222P�  � �� . . ��� $ arrow�� � �� �	#FF003366�� " 9�.���  � ��# � ����  � ��
 ���� ��  � ��$ Stretch=���  � ��$ Stretch=���  � ��
 ���� ��  � ��$ 1q���  � �� �� "	 9�.R��� #��z��#��z��#��z��$ 3�� . ._��� $ Vertical=�.+�d� $ 
HeaderSite��$��40��#��z��#��z��#��z��$	��Top=�#��z��#��z��8   #��z��#��z��#��z��#��z��#��z��$��1q�$��0��$��0��#��z��8�� #��z�� �� *
IsExpanded+$ TwoWay=� �� *TemplatedParent+�� $ 
ExpandSite��|�.  #��z��#��z��$y�	Collapsed=�#��z�� P�  �@9�
IsExpanded . . ��� $ 
ExpandSite�� � y�$ Visible=�P�  � {� .  . ��� # � �� � ��.+�9� $��Buckets��$ 9� 
IsExpanded$ . $��16(�#��[ $
��Left=�$	��Top=����� *ActualWidth+% �� ElementName$% userControl��$ OneWay=�.���� $ Vertical=�.+��� -   & ��Name$& buckets��
������ $��Stretch=�' �� SelectedIndex' �� *selectedBuketIndex+���� .R�� 
������ 
( � Tag( �� *DataContext+$
% root��) � ContextMenu) ��   Caliburn.Micro.Action*   TargetWithoutContext* �� + �� Path$+ PlacementTarget.Tag� �� *Self+.@�m� $��删除��$-! '[Event Click] = [Action deleteBucket()]��m� $��修改权限��$0! *[Event Click] = [Action changeBucketAcl()]�� �� $��16 ��� $��16 ��� $��* �.+��� $��../Images/diskdrive.png��$��-5q�$��1���� $��2��$��7 0 0 0q�$��16(����� *Name+9� $��0,10,0,0q�$ . $��任务��$��16(�$,! &[Event Expanded] = [Action expanded()]��#��[ $
��Left=�$	��Top=����� *ActualWidth+$% userControl��$ OneWay=�.���� 
������ $��0,0,0,0q�  AccentColorBrush#���  ' �� *SelectedViewIndex+.@��� -   , ��Name$, downloadingView��$3! -[Event Selected] = [Action downloadingView()]��$��24��.���  �� $��16 ��� $��16 ��� $��* �.+��� $��../Images/downloading.png��$��-5q�$��1���� $��7 0 0 0q�$��正在下载��$��2���� $��2��$
��Left=�$��	150,0,0,0q����� *DownloadingCount+�� -   $, uploadingView��$1! +[Event Selected] = [Action uploadingView()]��$��24��.���  �� $��16 ��� $��16 ��� $��* �.+��� $��../Images/uploading.png��$��-5q�$��1���� $��7 0 0 0q�$��2��$��正在上传���� $��2��$
��Left=�$��	150,0,0,0q����� *UploadingCount+�� -   $, compeletedView��$2! ,[Event Selected] = [Action compeletedView()]��$��24��.���  �� $��16 ��� $��16 ��� $��* �.+��� $��../Images/complete.png��$��-5q�$��1���� $��7 0 0 0q�$��2��$��	已完成���� $��2��$
��Left=�$��	150,0,0,0q����� *CompeletedCount+