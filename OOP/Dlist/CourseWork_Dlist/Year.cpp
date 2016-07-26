#include<iostream>
#include<iomanip>

using namespace std;

#include"Music.h"
#include"Rock.h"
#include"Metal.h"
#include"Year.h"

Year :: Year (char *inSong,char *inAuthor,char *r,char *me, int y):Metal (inSong,inAuthor,r,me)
{
	god=y;
}

void Year :: print (void)
{
	Metal::print();
	cout<<setw(17)<<"Year      : "<<god<<endl;
	
}



char *Year::getClassName(void)
{
	return "Year";
}
