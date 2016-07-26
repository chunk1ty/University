import java.awt.Color;
import java.awt.Graphics;

public class FilledRectangle extends Figures implements Shape
{	
	public FilledRectangle(int xInitial, int yInitial, int widthInitial, int heightInitial, Color colorInitial)
	{
		super(xInitial, yInitial, widthInitial, heightInitial, colorInitial);		
	}
	
	@Override
	public void drawShape(Graphics g) 
	{		
		int width = super.x1 - super.x2;
    	int height = super.y1 - super.y2;
    	
    	super.w = Math.abs( width );
    	super.h = Math.abs( height );
    	super.x = width < 0 ? super.x1 : super.x2;
    	super.y = height < 0 ? super.y1 : super.y2;
    	g.setColor(super.color);
    	g.fillRect( super.x, super.y, super.w, super.h);    	
	}
	
	public String toString()
	{		
		String colorAsString = null;
		StringBuffer sb = new StringBuffer();
		
		sb.append("Filled Rectangle : ");		
		sb.append("x = " + super.x);
		sb.append(" y = " + super.y);
		sb.append(" width = " + super.w);
		sb.append(" height = " + super.h);
		sb.append(" Color =" + super.ColorAsString(colorAsString));
		
		return sb.toString();
	}
}
