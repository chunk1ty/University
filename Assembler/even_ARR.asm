		name	"EVEN_ARR"
		org		100h
		jmp		BEGIN
;-------------------------------------
; Define variable
;-------------------------------------
msgBegAddr      DB	"BEG_ADDR:" , 20h, 24h
msgEndAddr		DB	09h, 09h, "END_ADDR:" , 20h, 24h
msgFinish		DB	0dh, 0ah, "Program end." , 24h
msgError		DB	0dh, 0ah, "ERROR - invalid address!" , 24h

;example array
array           DB		',', ' ', 0ah, 'ï', '5', 0ch, '7', 0eh, '9', 'a', 0dh, 'c', 'd', 0bh, 'f'

startAddr 		DW	?
endAddr			DW	?

;-------------------------------------
;Main program code
;-------------------------------------
BEGIN:
;Init:

;Set video mode:    
		mov 	ax, 3     ; text mode 80x25, 16 colors, 8 pages (ah=0, al=3)
		int 	10h       ; do it!

;Show first text line:
		lea 	ax,	msgBegAddr
		call	print
;Read array begin address:
		mov		cx,	4
		call	read
		mov     startAddr, bx
		
;Show next text line:
		lea 	ax,	msgEndAddr
		call	print
;Read array end address:
		mov		cx,	4
		call	read
		mov     endAddr, bx
		
;Call validate input param :
		call	valid
		
;Convert array elemenst to even:
		call	conv

;Show finish message:
		lea		ax,	msgFinish
		call	print
		
;On end wait to press KEY
		mov		ah,	0
		int		16h
		int		20h
;		ret

;-------------------------------------
;Program functions
;-------------------------------------
;Show text:
print:	mov		dx,ax  
        mov		ah,09h  
        int		21h    
		ret

;Convert ASCII to HEX:
a2h: 	add		al,40h		
		cbw
		and		ah,09h
		add		al,ah
		and		al,0fh
		ret

;Check correct HEX symbol:
check:	cmp		al,30h;
		jb		n
		cmp		al,3ah
		jb		y
		cmp		al,41h
		jb		n
		cmp		al,47h
		jb		y
		cmp		al,61h;
		jb		n
		cmp		al,67h
		jb		y
n:		jmp     badHex
y:		ret

;Read one kyeboard symbol:
badHex:	pop		bx
read:	mov		ah,0
		int		16h
		call    check
		push	ax
		mov		ah,0eh		
		int		10h
		loop	read
;Packed four readed ASCCI numbers:
		pop		ax
		call 	a2h
		mov 	bl,al
		pop		ax
		call	a2h
		mov		cl,4
		shl		al,cl
		or		bl,al
		pop		ax
		call 	a2h
		mov		bh,al
		pop		ax
		call 	a2h
		mov 	cl,4
		shl		al,cl
		or		bh,al
		ret

;Validate input param. :
valid:	mov		ax,	endAddr
		cmp		ax,	startAddr
		ja		pass
;Show bad input param. :
		lea 	ax,	msgError
		call	print
		mov		ah,	0
		int		16h
		int		20h
pass:	ret

;Convert array:
conv:	mov     cx, endAddr
        sub     cx, startAddr
        inc     cx
        mov     si, startAddr
		and		bx,	0
lp:		lodsb
;Check for even number:
		or  	al,	0
		jpe		even
        test    al, 10000000b 
        jz      zf
        and     al, 01111111b
        jmp     store
zf:     or      al, 10000000b
store:  mov     [si]-1, al
even:	loop	lp
		ret		
		