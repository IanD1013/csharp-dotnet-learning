void CenterRectangle(IRectangle rectangle)
{
    // move the rectangle to the center of the screen
}

LegacyRectangle legacyRectangle = new LegacyRectangle(0, 0, 100, 100);          
IRectangle rectangle = new LegacyRectangleAdapter(legacyRectangle);
CenterRectangle(rectangle);
    
    
    
