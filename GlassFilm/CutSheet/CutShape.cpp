#define _USE_MATH_DEFINES

#include <cmath>
#include <math.h>
#include <stdlib.h>
#include <Windows.h>

#include "CutShape.h"

#define LINE_MAP_SIZE 10

CutShape::CutShape()
{
	for (int i = 0; i < MAX_ANGLES; i++)
	{
		scans[i] = nullptr;
		sortedScans[i] = nullptr;
	}
}

CutShape::~CutShape()
{
	for (int i = 0; i < MAX_ANGLES; i++)
	{
		if (scans[i] != nullptr)
		{
			scans[i]->Clear();
			delete scans[i];
		}
	}
}

void CutShape::AddAngle(int angle, int width, int height, void* data)
{
	if (angle >= 0 && angle < MAX_ANGLES)
	{
		CutScan *cs = new CutScan(height, angle);
		scans[angle] = cs;

		cs->ScanImageData(width, height, data);
	}
}

CutScan* CutShape::GetSortedScan(int index)
{
	if (index >= 0 && index < MAX_ANGLES)
	{
		return scans[index];
	}

	return 0;
}

CutScan* CutShape::GetCurrentScan()
{
	return curScan;
}

CutScan* CutShape::GetScan(int angle)
{
	if (angle >= 0 && angle < MAX_ANGLES)
	{
		return scans[angle];
	}

	return 0;
}

void CutShape::SortAngles()
{
	for (int i = 0; i < MAX_ANGLES; i++)
	{
		sortedScans[i] = scans[i];
	}
}

void UpdateMap(LineList* lineMap, LineSegment** segments, int mapCount, float oy)
{
	LineSegment** seg = segments;

	for (int i = 0; i < mapCount; i++)
		lineMap[i].Reset();

	while (*seg)
	{
		LineSegment* s = *seg;

		float t1 = s->GetStart()->y;
		float t2 = s->GetEnd()->y;

		float y1 = t1;
		float y2 = t2;

		if (t1 > t2)
		{
			y1 = t2;
			y2 = t1;
		}

		y1 += oy;
		y2 += oy;

		int mapIndex = (int)floor(y1 / LINE_MAP_SIZE);

		do
		{
			if (mapIndex >= 0)
			{
				lineMap[mapIndex].AddSegment(s);
			}

			mapIndex++;

		} while ((mapIndex * LINE_MAP_SIZE) < y2);

		seg++;
	}
}

void RotatePoints(float angle, LinePoint** src, LinePoint** dst)
{
	float s = sin(angle);
	float c = cos(angle);

	LinePoint** sp = src;
	LinePoint** dp = dst;

	while (*sp)
	{
		LinePoint* ss = *sp;
		LinePoint* dd = *dp;

		dd->x = ss->x * c - ss->y * s;
		dd->y = ss->x * s + ss->y * c;

		sp++;
		dp++;
	}
}

void CutShape::BuildCurrentScan(float width, float height, float* poly, int pointCount, float angle)
{
	LinePoint** points = new LinePoint*[pointCount + 1];
	LinePoint** original = new LinePoint*[pointCount + 1];

	float* p = poly;
	for (int i = 0; i < pointCount; i++)
	{
		LinePoint *pt = new LinePoint();

		pt->x = *p++;
		pt->y = *p++;

		points[i] = pt;

		original[i] = new LinePoint();
		original[i]->x = pt->x;
		original[i]->y = pt->y;
	}

	points[pointCount] = nullptr;
	original[pointCount] = nullptr;

	LineSegment** segments = new LineSegment*[pointCount];

	LinePoint **pt = points;
	LinePoint *last = *pt;

	float oy = height / 2;

	int si = 0;
	while (*pt)
	{
		pt++;

		if (*pt)
		{
			LineSegment* seg = new LineSegment(last, *pt);
			segments[si++] = seg;
			last = *pt;
		}
	}

	segments[si] = nullptr;

	int mapCount = (int)(height / LINE_MAP_SIZE) + 1;

	LineList* lineMap = new LineList[mapCount];

	RotatePoints((float)(angle * M_PI) / 180.0f, original, points);
	UpdateMap(lineMap, segments, mapCount, oy);

	curScan = new CutScan((int)height, 360);

	curScan->ScanLineMap(this->width, this->height, lineMap, LINE_MAP_SIZE);

	for (int i = 0; i < pointCount; i++)
	{
		delete points[i];
		delete original[i];
	}

	delete[] points;
	delete[] original;
	delete[] lineMap;

	LineSegment** seg = segments;

	while (*seg)
	{
		if (*seg)
			delete *seg;

		seg++;
	}

	delete[] segments;
}

void CutShape::BuildScansFromPolygon(float width, float height, float* poly, int pointCount)
{
	this->width = width;
	this->height = height;

	LinePoint** points = new LinePoint*[pointCount + 1];
	LinePoint** original = new LinePoint*[pointCount + 1];

	float* p = poly;
	for (int i = 0; i < pointCount; i++)
	{
		LinePoint *pt = new LinePoint();

		pt->x = *p++;
		pt->y = *p++;

		points[i] = pt;

		original[i] = new LinePoint();
		original[i]->x = pt->x;
		original[i]->y = pt->y;
	}

	points[pointCount] = nullptr;
	original[pointCount] = nullptr;

	LineSegment** segments = new LineSegment*[pointCount];

	LinePoint **pt = points;
	LinePoint *last = *pt;

	float oy = height / 2;

	int si = 0;
	while (*pt)
	{
		pt++;

		if (*pt)
		{
			LineSegment* seg = new LineSegment(last, *pt);
			segments[si++] = seg;
			last = *pt;
		}
	}

	segments[si] = nullptr;

	int mapCount = (int)(height / LINE_MAP_SIZE) + 1;

	LineList* lineMap = new LineList[mapCount];

	float angle = 0;
	float rad = (float)M_PI / 180.0f;

	for (int i = 0; i < MAX_ANGLES; i++)
	{
		UpdateMap(lineMap, segments, mapCount, oy);

		CutScan *cs = new CutScan((int)height, i);
		scans[i] = cs;

		cs->ScanLineMap(this->width, this->height, lineMap, LINE_MAP_SIZE);

		angle += rad;
		RotatePoints(angle, original, points);
	}

	SortAngles();

	for (int i = 0; i < pointCount; i++)
	{
		delete points[i];
		delete original[i];
	}

	delete[] points;
	delete[] original;
	delete[] lineMap;

	LineSegment** seg = segments;

	while (*seg)
	{
		if (*seg)
			delete *seg;

		seg++;
	}

	delete[] segments;
}