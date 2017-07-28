#pragma once

#include "CutSheet.h"
#include "CutScan.h"

#define CUT_EXPORT __declspec(dllexport)
#define CUT_API __stdcall

extern "C" {

	CUT_EXPORT unsigned int CUT_API CreateSheet(int size); 
	CUT_EXPORT unsigned int CUT_API CreateShape(unsigned int sheet, unsigned int id);
	CUT_EXPORT unsigned int CUT_API DeleteShape(unsigned int sheet, unsigned int id);
	CUT_EXPORT void CUT_API ResetSheet(unsigned int sheet, int size);
	CUT_EXPORT void CUT_API AddAngle(unsigned int sheet, unsigned int shape, int angle, int width, int height, void* data);
	CUT_EXPORT void CUT_API Plot(unsigned int sheet, unsigned int shape, int angle, int x, int y);
	CUT_EXPORT void CUT_API TestShape(unsigned int sheet, unsigned int shape, CutTestResult* result);
	CUT_EXPORT void CUT_API SortAngles(unsigned int sheet, unsigned int shape);
	CUT_EXPORT int CUT_API GetSegmentCount(unsigned int sheet, unsigned int shapeId, int angle);
	CUT_EXPORT void CUT_API BuildScansFromPolygon(unsigned int sheet, unsigned int shapeId, float width, float height, float* poly, int pointCount);
}

