#pragma once

#include "CutSegment.h"
#include "CutSegmentList.h"
#include "LineList.h"

#define MAX_CUT_LINES 32766

struct SegmentData {
	int start;
	int end;
	int line;
};

struct ScanData {
	int angle;
	int segmentCount;
	SegmentData *segments;
};

class CutScan {
	int angle = 0;
	int width = 0;
	int lineCount = 0;		
	int verticalCenter = 0;
	int horizontalCenter = 0;
	int boxLeft = 0;
	int boxRight = 0;
	int boxTop = 0;
	int boxBottom = 0;
	int middleMin = 0;
	int middleMax = 0;
	int middleLen = 0;
	CutSegmentList* segments[MAX_CUT_LINES];
	void DeleteSegments();

	int* startPos = nullptr;
public:
	CutScan(int lines, int angle);
	~CutScan();

	void Clear();
	void ScanImageData(int width, int height, void* data);
	void ScanLineMap(float w, float h, LineList *list, int mapSize);
	void AddSegment(int line, int start, int end);
	void Normalize(int width, int height);

	int GetCenter();
	int GetLeft();
	int GetRight();
	int GetTop();
	int GetBottom();
	int GetWidth();
	int GetLineCount();
	int GetSegmentCount();
	int GetAngle() { return angle; }
	int* GetFirstPos() { return startPos; }
	bool TestLimits(int x, int y, int top, int left, int bottom);
	int GetMiddleMin();
	int GetMiddleMax();
	int GetMiddleLen();



	CutSegmentList** GetSegments();
};

