#pragma once

#include "CutShape.h"

#ifdef WIN32
#include <Windows.h>
#endif

extern "C" {
	typedef struct {
		int x;
		int y;
		int angle;
		int maxx;
		bool resultOk;
	} CutTestResult;
}

class CutSheet {
	
	typedef struct ShapeHolder {
		unsigned int id = 0;
		CutShape* shape;
		ShapeHolder *next;
	} ShapeHolder;
	
	int shapeCount = 0;
	ShapeHolder* first;
	ShapeHolder* last;

	CutSegmentList *freeSpace = nullptr;
	CutSegmentList *usedSpace = nullptr;
	CutSegment *endSpace = nullptr;
	int* endPos = nullptr;

	int minDist = 00;
	int lines = 1520;

	bool foundPos = false;
	CutTestResult result;
	int fullTestCount = 0;

	void SetResult(int angle, int x, int y, int maxx);

	void TestFreeSpace(CutShape* shape);
	void TestEndSpace(CutShape* shape);
	bool TestFullScan(CutScan* scan, int x, int y);
public:
	CutSheet(int size);

	CutShape* GetShape(unsigned id);
	int CreateShape(unsigned int id);
	bool TestShape(CutShape* shape, CutTestResult *res);
	void Reset(int size);

	void Plot(CutScan *scan, int x, int y);
	void Plot(CutShape* shape, int angle, int x, int y);

	void AddAngle(unsigned shape, int angle, int width, int height, void* data);

#ifdef WIN32
	void Render(HDC hdc, int x, int y);
#endif
};
