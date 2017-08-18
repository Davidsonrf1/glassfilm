#include <Windows.h>
#include "CutLibDLL.h"

#define SHEET_BUFFER_LEN 1024

unsigned int sheetCount = 0;
CutSheet** sheets = NULL;

// Interface da DLL

extern "C" {

	CUT_EXPORT unsigned int CUT_API CreateSheet(int size)
	{
		if (sheetCount == 0)
		{
			sheets = new CutSheet*[SHEET_BUFFER_LEN];
		}

		if (sheetCount < SHEET_BUFFER_LEN)
		{
			sheets[sheetCount] = new CutSheet(size);
		}

		return sheetCount++;
	}

	CUT_EXPORT void CUT_API ResetSheet(unsigned int sheet, int size)
	{
		if (sheet < sheetCount)
		{
			sheets[sheet]->Reset(size);
		}
	}

	CUT_EXPORT unsigned int CUT_API DeleteShape(unsigned int sheet, unsigned int id)
	{
		if (sheet < sheetCount)
		{
			return sheets[sheet]->DeleteShape(id);
		}

		return 0;
	}

	CUT_EXPORT unsigned int CUT_API CreateShape(unsigned int sheet, unsigned int id)
	{
		if (sheet < sheetCount)
		{
			return sheets[sheet]->CreateShape(id);
		}

		return 0xffffffff;
	}

	CUT_EXPORT void CUT_API AddAngle(unsigned int sheet, unsigned int shape, int angle, int width, int height, void* data)
	{
		if (sheet < sheetCount)
		{
			sheets[sheet]->AddAngle(shape, angle, width, height, data);
		}
	}

	void DrawLine(HDC hDC, int sx, int sy, int ex, int ey)
	{
		MoveToEx(hDC, sx, sy, NULL);
		LineTo(hDC, ex, ey);
	}

	CUT_EXPORT void CUT_API RenderScan(unsigned int sheet, unsigned int shapeId, int angle, int x, int y, HDC hDC)
	{
		if (sheet < sheetCount)
		{
			CutShape* shape = sheets[sheet]->GetShape(shapeId);
			CutScan* scan = shape->GetScan(angle);

			if (scan != nullptr)
			{
				HPEN hLinePen, hLinePen2;
				COLORREF qLineColor;
				qLineColor = RGB(255, 100, 150);
				hLinePen = CreatePen(PS_SOLID, 1, qLineColor);
				SelectObject(hDC, hLinePen);

				CutSegmentList** scanLines = scan->GetSegments();
				int lineCount = scan->GetLineCount();

				for (int i = 0; i < lineCount; i++)
				{
					CutSegment *seg = scanLines[i]->GetFirst();

					if (seg)
					{
						do
						{
							int sx = seg->GetStart() + x;
							int ly = (i - scan->GetCenter()) + y;
							int ex = seg->GetEnd() + x;							
							
							DrawLine(hDC, sx, ly, ex, ly);
						} while (seg = seg->GetNext());
					}
				}

				DrawLine(hDC, scan->GetLeft()+x, scan->GetTop()+y, scan->GetRight()+x, scan->GetTop()+y);
				DrawLine(hDC, scan->GetRight() + x, scan->GetTop() + y, scan->GetRight() + x, scan->GetBottom() + y);
				DrawLine(hDC, scan->GetRight() + x, scan->GetBottom() + y, scan->GetLeft() + x, scan->GetBottom() + y);
				DrawLine(hDC, scan->GetLeft() + x, scan->GetBottom() + y, scan->GetLeft() + x, scan->GetTop() + y);
				
				qLineColor = RGB(0, 255, 0);
				hLinePen2 = CreatePen(PS_SOLID, 1, qLineColor);

				int verticalCenter = scan->GetCenter();

				SelectObject(hDC, hLinePen2);
				DrawLine(hDC, scan->GetLeft() + x, y, scan->GetRight() + x, y);

				DeleteObject(hLinePen);
				DeleteObject(hLinePen2);
			}
			
			
		}
	}

	CUT_EXPORT void CUT_API RenderSheet(unsigned int sheet, int x, int y, HDC hDC)
	{
		if (sheet < sheetCount)
		{
			sheets[sheet]->Render(hDC, x, y);
		}
	}

	CUT_EXPORT void CUT_API PlotCurrentScan(unsigned int sheet, unsigned int shape, int x, int y)
	{
		if (sheet < sheetCount)
		{
			sheets[sheet]->Plot(sheets[sheet]->GetShape(shape)->GetCurrentScan(), x, y);
		}
	}

	CUT_EXPORT void CUT_API Plot(unsigned int sheet, unsigned int shape, int angle, int x, int y)
	{
		if (sheet < sheetCount)
		{
			sheets[sheet]->Plot(sheets[sheet]->GetShape(shape), angle, x, y);
		}
	}

	CUT_EXPORT void CUT_API SortAngles(unsigned int sheet, unsigned int shape)
	{
		if (sheet < sheetCount)
		{
			CutShape* s = sheets[sheet]->GetShape(shape);

			if (s != nullptr)
				s->SortAngles();
		}
	}

	void CUT_API TestShape(unsigned int sheet, unsigned int shapeId, CutTestResult* result, bool forceAngle, int angle)
	{
		if (sheet < sheetCount)
		{
			CutShape* shape = sheets[sheet]->GetShape(shapeId);

			CutTestResult res;
			res.resultOk = false;
			sheets[sheet]->TestShape(shape, &res, forceAngle, angle);

			if (res.resultOk) 
			{
				result->angle = res.angle;
				result->x = res.x;
				result->y = res.y;
				result->maxx = res.maxx;
				result->resultOk = true;
			}
		}
	}

	CUT_EXPORT int CUT_API GetSegmentCount(unsigned int sheet, unsigned int shapeId, int angle)
	{
		if (sheet < sheetCount)
		{
			CutShape* shape = sheets[sheet]->GetShape(shapeId);
			CutScan* scan = shape->GetScan(angle);

			return scan->GetSegmentCount();
		}

		return 0;
	}

	CUT_EXPORT void CUT_API BuildCurrentScan(unsigned int sheet, unsigned int shapeId, float width, float height, float* poly, int pointCount, float angle)
	{
		if (sheet < sheetCount)
		{
			CutShape* shape = sheets[sheet]->GetShape(shapeId);
			shape->BuildCurrentScan(width, height, poly, pointCount, angle);
		}
	}

	CUT_EXPORT void CUT_API BuildScansFromPolygon(unsigned int sheet, unsigned int shapeId, float width, float height, float* poly, int pointCount)
	{
		if (sheet < sheetCount)
		{
			CutShape* shape = sheets[sheet]->GetShape(shapeId);
			shape->BuildScansFromPolygon(width, height, poly, pointCount);
		}
	}

}