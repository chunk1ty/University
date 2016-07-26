#ifndef MUSIC_H
#define MUSIC_H

class Music
{
public:
	Music ( char *inSong = "Pass Out", char *inAuthor="Tinie Tempah");
	virtual ~Music() {delete song; delete author;}
	virtual void print (void);
	virtual char *getClassName (void);
	void link(Music * );
	void printlist(Music *);
	void printlistBack(Music *);
	
protected:
	char *song;
	char *author;
	Music *next;
	Music *prev;
};

#endif