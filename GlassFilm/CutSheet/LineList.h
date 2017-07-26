#pragma once

struct LinePoint
{
	float x = 0;
	float y = 0;
};

class LineSegment
{
	LinePoint* start = nullptr;
	LinePoint* end = nullptr;
public:
	LineSegment(LinePoint* s, LinePoint* e);	
	bool HorizontalLineCross(float hline, int* crossCount, float* crossPoint1, float* crossPoint2);

	inline LinePoint* GetStart() { return start; }
	inline LinePoint* GetEnd() { return end; }
};

class LineList
{
	LineSegment** lines;
	unsigned int capacity;
	unsigned int lineCount;
public:
	LineList();
	~LineList();

	void AddSegment(LineSegment* seg);	
	void Clear();
	void Reset();

	inline LineSegment** GetLines() { return lines; }
	inline unsigned int GetCount() { return lineCount; }

	int FillCrossPoints(float* list, int max, int hline);
};

class LineMap 
{
	LineList* lines = nullptr;
	unsigned int lineCount = 0;
public:
	LineMap(unsigned int count);
	~LineMap();
};