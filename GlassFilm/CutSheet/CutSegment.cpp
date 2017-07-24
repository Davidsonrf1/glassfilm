#include "CutSegment.h"
#include "CutSegmentList.h"

CutSegment::CutSegment()
{
	line = 0;
	SetSE(0, 0); 
}

CutSegment::CutSegment(int line, int start, int end)
{
	this->line = line;
	SetSE(start, end);
}

void CutSegment::SetSE(int s, int e)
{
	if (s < e) 
	{
		start = s;
		end = e;
	}
	else 
	{
		start = e;
		end = s;
	}

	len = end - start;
}

void CutSegment::SetStart(int start)
{ 
	SetSE(start, this->end);
}

void CutSegment::SetEnd(int end)
{ 
	SetSE(this->start, end);
}

void CutSegment::SetLine(int line) 
{ 
	CutSegment::line = line; 
}

int CutSegment::GetLen() 
{ 
	return len; 
}

int CutSegment::GetStart() 
{ 
	return start; 
}

int CutSegment::GetEnd() 
{ 
	return end; 
}

int CutSegment::GetLine() 
{ 
	return line; 
}

bool CutSegment::Intersect(int start, int end)
{
	if (start <= this->end && end >= this->start)
		return true;
	/*
	int s = this->start;
	int e = this->end;



	if (start >= s && start <= e)
		return true;

	if (end >= s && end <= e)
		return true;

	if (s >= start && s <= end)
		return true;

	if (e >= start && e <= end)
		return true;
		*/
	return false;
}

void CutSegment::Normalize(int verticalCenter)
{
	SetSE(start - verticalCenter, end - verticalCenter);
}

void CutSegment::SetNext(CutSegment* n)
{
	next = n;
}

CutSegment* CutSegment::GetNext()
{
	return next;
}

void CutSegment::Plot(void* seg, int x)
{
	if (seg != nullptr)
		((CutSegmentList*)seg)->AddSegment(line, start + x, end + x);
}
