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

		int c = 0;
		float cross1 = 0;
		float cross2 = 0;

		if (seg->HorizontalLineCross((float)hline, &c,  &cross1, &cross2))
		{
			list[count++] = cross1;

			if (c == 2)
			{
				list[count++] = cross2;
			}
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
}

bool LineSegment::HorizontalLineCross(float hline, int* crossCount, float* crossPoint1, float* crossPoint2)
{
	*crossPoint1 = 0;
	*crossPoint2 = 0;
	*crossCount = 1;

	float dy = end->y - start->y;

	if (dy != 0)
	{
		*crossPoint1 = ((hline - start->y) / dy) * (end->x - start->x) + start->x;

		if (start->x < end->x)
		{
			if (*crossPoint1 >= start->x && *crossPoint1 < end->x)
				return true;
		}
		else
		{
			if (*crossPoint1 > end->x && *crossPoint1 <= start->x)
				return true;
		}
	}
	else
	{
		if (hline == start->y)
		{
			*crossPoint1 = start->x;
			*crossPoint2 = end->y;
			*crossCount = 2;

			return true;
		}
	}

	return false;
}
