import java.awt.Color;
import java.awt.Graphics;

public class Line extends Figures implements Shape
{	
	public Line(int x1Initial, int y1Initial, int x2Initial, int y2Initial, Color colorInitial) 
	{
		super(x1Initial, y1Initial, x2Initial, y2Initial, colorInitial);		
	}

	public void drawShape(Graphics g) 
	{		
		g.setColor(super.color);
		g.drawLine(super.x1, super.y1, super.x2, super.y2);			
	}
	
	public String toString()
	{		
		String colorAsString = null;
		StringBuffer sb = new StringBuffer();
		
		sb.append("Line : ");		
		sb.append("x1 = " + super.x1);
		sb.append(" y1 = " + super.y1);
		sb.append(" x2 = " + super.x2);
		sb.append(" y2 = " + super.y2);
		sb.append(" Color =" + super.ColorAsString(colorAsString));
		
		return sb.toString();
	}	
}
