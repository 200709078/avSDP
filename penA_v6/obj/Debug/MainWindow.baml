   M S B A M L   `   `   `  ����B  >penA_v6, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:penA_v6penA_v6  5   	       penA_v6.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:penA_v6     5   	   -   5          �Name$  wMain��5   	    � Title$ penA - made by mADEMatik��5   	   
�������$	��450��6A   $	��800��6N    � WindowStartupLocation$ CenterScreen=�6Z    � WindowState$ 	Maximized=�6   .��5	      � -   5        $��grdMain��5	      .+�5
   
   �� -   5        $��inkBoard��5
      6   �� 5   
   -   5        $��stckMain��5       �� Orientation$ Vertical=�6%   .+�5      �� -   5         ��Name$ stcTop��5      $ 
Horizontal=�6)   $��50��6B   $	��Top=�6N   .+�5      �� -   5         ��Name$ btnDraw��5      $��70��6*      roundButton#��[  65   $
��DRAW��6Z    J� RenderTransformOrigin$ 0.496,1.057&�6   �� 5      -   5        $ 
btnPicture��5      $��70��6-   #��[  68   $��PICTURE��6]   5      �� 5      -   5        $ 	stckFiles��5      $ 
Horizontal=�6,   $��10q�6E   5      5      �� 5   
   -	   5         ��Name$ imgSS��5      $y�Hidden=�6   �� 5   
   -
   5        $��stckDraw��5      $ 
Horizontal=�6%   $��50��6>   $��Bottom=�6J   $��Center=�6e   $��10q�6�   .+�5      �� -   5        $ btnMainPage��5      #��[  6*   $
��Main��6O   6^   �� 5      -   5        $ btnSize��5      #��[  6&   $��Size 2��6K   6\   �� 5      -   5        $ btnColor��5        roundButtonRed#��[ 6'   $��Color��6O   6_   �� 5      -   5        $ btnMode��5      #��[  6&   $	��Pen��6K   6Y   �� 5      -   5        $ btnShape��5        roundButtonGray#��[ 6'   $
��Line��6P   6_   �� 5      -   5        $ btnSS��5      #��[  6$   $��SS��6I   6V   �� 5      -   5        $ btnUndo��5      #��[ 6&   $
��Undo��6O   6^   �� 5      -   5        $ btnRedo��5      #��[ 6&   $
��Redo��6O   6^   �� 5      -   5        $ btnClear��5      #��[ 6'   $��Clear��6P   6`   5      5       5!      