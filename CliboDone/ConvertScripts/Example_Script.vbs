O p t i o n   E x p l i c i t  
  
 '   = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =  
 '   M a i n   e n t r y   p o i n t .  
 '  
 '   A r g s       :   t e m p l a t e                     . . .   T h e   c o n t e n t s   o f   t h e   T e m p l a t e . t x t   f i l e .  
 '                 :   c l i p b o a r d C o n t e n t s   . . .   C l i p b o a r d   c o n t e n t s .  
 '   R e t u r n   :   R e s u l t   o f   c o n v e r s i o n .  
 '   = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =  
 F u n c t i o n   M a i n ( t e m p l a t e C o n t e n t s ,   c l i p b o a r d C o n t e n t s )  
  
 	 '   R e t u r n   v a r i a b l e  
 	 D i m   r e t  
 	  
 	 '   R e p l a c e   w i t h   c o n t e n t s   v a r i a b l e   i n   t e m p l a t e   s t r i n g .  
 	 I f   N o t   I s E m p t y ( t e m p l a t e C o n t e n t s )   T h e n  
 	 	 r e t   =   R e p l a c e ( t e m p l a t e C o n t e n t s ,   " $ { c l i p b o a r d C o n t e n t s } " ,   c l i p b o a r d C o n t e n t s )  
 	 E l s e  
 	 	 r e t   =   c l i p b o a r d C o n t e n t s  
 	 E n d   I f  
  
 	 '   R e t u r n  
 	 M a i n   =   r e t  
  
 E n d   F u n c t i o n  
 