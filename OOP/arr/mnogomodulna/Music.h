#ifndef MUSIC_H
#define MUSIC_H

class Music
{
public:
	Music ( char *,char *);
	virtual ~Music() {delete song; delete author; }
	virtual void print (void);
	virtual char *getClassName (void);
protected:
	char *song;
	char *author;
	
};

#endif