#ifndef FOLKMUSIC_H
#define FOLKMUSIC_H


class FolkMusic : public Music
{
public:
	FolkMusic( char *inSong="Eleno Mome",char *inAuthor="Hristina Boteva",char *fm="folk");
	~FolkMusic (void) {delete fmKind;}
	virtual void print (void);
	virtual char *getClassName (void);
private:
	char *fmKind;
};

#endif