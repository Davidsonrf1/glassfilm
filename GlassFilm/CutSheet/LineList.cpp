#include <stdlib.h>

#include "LineList.h"

#define LINE_LIST_BLOCK_SIZE 64

LineList::LineList()
{
	lineCount = 0;
	capacity = LINE_LIST_BLOCK_SIZE;

	Clear();
}

LineList::~LineList()
{
	Clear();
}

void LineList::AddSegment(LineSegment* seg)
{
	if (lineCount == capacity)
	{
		capacity += LINE_LIST_BLOCK_SIZE;
		lines = (LineSegment**)realloc(lines, sizeof(LineSegment*) * (capacity + 1));
	}

	lines[lineCount++] = seg;
	lines[lineCount] = nullptr;
}

void LineList::Reset()
{
	lineCount = 0;
	lines[0] = nullptr;
}

void LineList::Clear()
{
	if (lineCount > 0)
	{
		free(lines);
		lines = nullptr;
	}

	lines = (LineSegment**)malloc(sizeof(LineSegment*) * (LINE_LIST_BLOCK_SIZE + 1));

	capacity = LINE_LIST_BLOCK_SIZE;
	lineCount = 0;
}

int LineList::FillCrossPoints(float* list, int max, int hline)
{
	int count = 0;

	LineSegment** p = lines;

	while (*p)
	{
		LineSegment* seg = *p;

		if (count >= max)
			break;

		if (seg->HorizontalLineCross(hline, &list[count]))
		{
			count++;
		}

		p++;
	}

	return count;
}

LineMap::LineMap(unsigned int count)
{
	lines = new LineList[count];
}

LineMap::~LineMap()
{
	if (lineCount > 0)
		free(lines);
}

/*********************  LINE SEGMENT  *********************/
LineSegment::LineSegment(LinePoint* s, LinePoint* e)
{
	start = s;
	end = e;

	CalcSlope();
}

bool LineSegment::HorizontalLineCross(float hline, float* crossPoint)
{
	float y1 = start->y;
	float y2 = end->y;

	bool cross = false;

	if (y2 > y1)
	{
		if (hline >= y1 && hline < y2)
			cross = true;
	}
	else
	{
		if (hline > y2 && hline <= y1)
			cross = true;
	}

	if (cross)
	{
		float dy = y2 - y1;

		if (dy != 0)
		{
			*crossPoint = ((hline - y1) / dy) * (end->x - start->y);
		}
		else
		{
			*crossPoint = y1;
		}
	}

	return cross;
}

void LineSegment::CalcSlope()
{
	float dx = start->x - end->x;
	float dy = start->y - end->y;

	if (dy != 0) 
	{
		slope = dx / dy;
	}
	else
	{
		slope = 0;
	}
}