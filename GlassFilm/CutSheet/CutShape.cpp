#define _USE_MATH_DEFINES

#include <cmath>
#include <math.h>
#include <stdlib.h>
#include <Windows.h>

#include "CutShape.h"

#define LINE_MAP_SIZE 10

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
		return sortedScans[index];
	}

	return 0;
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

	/*
	for (int i = 0; i < MAX_ANGLES; i++)
	{
		CutScan* s = sortedScans[i];
		int sw = s->GetWidth();

		for (int j = 0; j < MAX_ANGLES; j++)
		{			
			CutScan* d = scans[j];

			if (d->GetWidth() >= sw)
			{
				//sortedScans[i] = d;
				//sortedScans[j] = s;

				//s = d;
			}
		}
	}
	*/
}

void UpdateMap(LineList* lineMap, LineSegment** segments, int mapCount, float oy)
{
	LineSegment** seg = segments;

	for (int i = 0; i < mapCount; i++)
		lineMap[i].Reset();

	while (*seg)
	{
		LineSegment* s = *seg;
		s->CalcSlope();

		float t1 = s->GetStart()->y;
		float t2 = s->GetEnd()->y;

		float y1 = (t1 < t2 ? t1 : t2) + oy;
		float y2 = (t1 > t2 ? t2 : t1) + oy;
		
		int mapIndex = (int)floor(y1 / LINE_MAP_SIZE);

		do 
		{
			if (mapIndex > 0)
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

void CutShape::BuildScansFromPolygon(float width, float height, float* poly, int pointCount)
{
	this->width = width;
	this->height = height;

	LinePoint** points = new LinePoint*[pointCount + 1];
	LinePoint** transformed = new LinePoint*[pointCount + 1];
	
	float* p = poly;
	for (int i = 0; i < pointCount; i++)
	{
		LinePoint *pt = new LinePoint();

		pt->x = *p++;
		pt->y = *p++;

		points[i] = pt;
		transformed[i] = new LinePoint();
	}

	points[pointCount] = nullptr;

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
			seg->CalcSlope();

			segments[si++] = seg;
			last = *pt;
		}
	}

	int time = GetTickCount();

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

		cs->ScanLineMap(this->width, this->height, lineMap);
		
		angle += rad;
    	RotatePoints(angle, points, transformed);
	}

	time = GetTickCount() - time;
}