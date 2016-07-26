#ifndef ROCK_H
#define ROCK_H



class Rock : public Music
{
public:
	Rock ( char *inSong="Smelles Like Tenn Spirit",char*inAuthor="Nirvana",char*r="rock");
	~Rock () {delete rKind;}
	virtual void print (void);
	virtual char *getClassName (void);
protected:
	char *rKind;
};

#endif