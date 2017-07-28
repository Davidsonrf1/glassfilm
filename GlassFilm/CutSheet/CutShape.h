#pragma once

#include "CutScan.h"
#include "LineList.h"

#define MAX_ANGLES 360


class CutShape {
	CutScan* scans[MAX_ANGLES];
	CutScan* sortedScans[MAX_ANGLES];

	float width = 0;
	float height = 0;

	LineSegment** segments = nullptr;
public:
	CutShape();
	~CutShape();

	void AddAngle(int angle, int width, int height, void* data);

	CutScan* GetScan(int angle);
	CutScan* GetSortedScan(int index);
	void SortAngles();

	void BuildScansFromPolygon(float width, float height, float* poly, int pointCount);
	void EndShape();
};
