   M S B A M L   `   `   `  ���� H  DMahApps.Metro, Version=0.9.0.0, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  I EOssClientMetro, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:OssClientMetro.ControlsOssClientMetro.Controls $   OssClientMetro.Views.LoginView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     :controls%clr-namespace:OssClientMetro.Controls     $
��Auto��$
��Auto�����	#ffe5e5e5$��Microsoft YaHei)�  � 	Resources   �� MergedDictionaries �� Source��  �� $H Bpack://application:,,,/MahApps.Metro;component/Styles/Colours.xaml?��� $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?��� $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?��� $e _pack://application:,,,/MahApps.Metro;component/Styles/Controls.AnimatedSingleRowTabControl.xaml?��� $I Cpack://application:,,,/MahApps.Metro.Resources;component/Icons.xaml?�%    .���� $��0,0,2,0q�._�� .+��� -    ��Name$ UserName��*   $MahApps.Metro.Controls.TextboxHelper   	Watermark$ 请输入AccessId��   ClearTextButton . $	��Top=�$
��Left=�$	��204��$��Center=�$��
23,207,0,0q�G� -    G�Name$ UserPassword��$ 请输入AccessKey��$��Center=�$��
23,247,0,0q�2  ,OssClientMetro.Controls.PasswordBoxAssistant   BindPassword . $
��Left=�$	��204��$��20��$	��Top=�   BoundPassword �� 	 �� Path$	 UserPassword�
 �� Mode$
 TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� -    ��Name$ login��   SquareButtonStyle#���   $��登 录��$
��Left=�$��152,410,0,0q�$	��Top=�$��75�� J� RenderTransformOrigin$ 0.507,1.364&�$��Microsoft YaHei)��� -    ��Name$ RememberKey��$��记住密码��$
��Left=�$��
28,285,0,0q�$	��Top=��� -   $ 	AutoLogin��$��自动登录��$
��Left=�$��135,285,0,0q�$	��Top=��� -    ��Name$ Status��
���  ��$
��Left=�$��83��$��
23,328,0,0q�$
��Wrap=�$	��Top=�$	��204���� $
��Left=�$	��100��$��	85,55,0,0q�$	��Top=�$��80��$��../Images/2.ico��� ���	#ffe5e5e5 J� Opacity$	 0.8Q�y��� *ProgressActive+  BoolToVisibleConverter �� 	Converter# [ .+�)   #MahApps.Metro.Controls.ProgressRing  $��Center=�$��	0,201,0,0q�$	��Top=�$��60��$��60��   IsActive �� *ProgressActive+