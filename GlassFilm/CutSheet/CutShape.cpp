#include "CutShape.h"

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