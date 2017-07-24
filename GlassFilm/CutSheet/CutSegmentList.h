#pragma once
#include "CutSegment.h"

class CutSegmentList
{
	int count;
	CutSegment* first;
	CutSegment* last;
public:
	CutSegmentList();
	void Clear();
	void AddSegment(int line, int start, int end);
	int GetCount();
	int Remove(CutSegment *seg);
	CutSegment* GetFirst() const { return first; }
	void Merge(int minDist);
	void GetMinMax(int* min, int* max);
	void Plot(CutSegmentList *dest, int x);
};
