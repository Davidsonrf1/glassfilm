#pragma once

class CutSegment{
	int line;
	int start;
	int end;
	int len;

	void SetSE(int s, int e);

	CutSegment* next;

	
public:
	CutSegment();
	CutSegment(int line, int start, int end);

	void SetStart(int start);
	void SetEnd(int end);
	void SetLine(int line);

	int GetLen();
	int GetStart();
	int GetEnd();
	int GetLine();

	void SetNext(CutSegment* n);
	CutSegment* GetNext();

	bool Intersect(int start, int end);
	void Normalize(int verticalCenter);

	void Plot(void* seg, int x);
};

