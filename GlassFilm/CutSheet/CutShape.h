#pragma once

#include "CutScan.h"

#define MAX_ANGLES 360

class CutShape {
	CutScan* scans[MAX_ANGLES];
	CutScan* sortedScans[MAX_ANGLES];
public:
	void AddAngle(int angle, int width, int height, void* data);
	CutScan* GetScan(int angle);
	CutScan* GetSortedScan(int index);
	void SortAngles();
};
