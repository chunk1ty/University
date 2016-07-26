#ifndef CLASSIC_H
#define CLASSIC_H

class Classic : public Music
{
public:
	Classic( char *inSong="Moonlight Sonata" ,char *inAuthor="Ludwing v.Beethoven" , char * c="classic");
	~Classic() {delete cKind;}
	virtual void print (void);
	virtual char *getClassName (void);
private:
	char *cKind;
};

#endif