#include "CutSheet.h"
#include "CutSegment.h"
#include "CutSegmentList.h"

#include <stdlib.h>
#include <stdio.h>

CutSheet::CutSheet(int size)
{
	shapeCount = 0;
	first = nullptr;
	last = nullptr;

	lines = size;

	Reset(lines);
}

void CutSheet::AddAngle(unsigned shape, int angle, int width, int height, void* data)
{
	CutShape *s = GetShape(shape);

	if (s) {
		s->AddAngle(angle, width, height, data);
	}
}

CutShape * CutSheet::GetShape(unsigned id)
{
	ShapeHolder *found = nullptr;
	ShapeHolder *p = first;

	if (p)
	{
		do
		{
			if (p->id == id)
				found = p;
		} while ((p = p->next) && !found);
	}

	if (found)
		return found->shape;

	return nullptr;
}

int CutSheet::DeleteShape(unsigned int id)
{
	ShapeHolder *found = nullptr;
	ShapeHolder *p = first;
	ShapeHolder *before = nullptr;

	if (p)
	{
		do
		{
			if (p->id == id)
				found = p;

			before = p;
		} while ((p = p->next) && !found);
	}

	if (found) 
	{
		if (before == nullptr)
		{
			first = found->next;
		}
		else
		{
			before->next = found->next;

			if (before->next == nullptr)
				last = before;
		}

		delete found->shape;
		delete found;

		shapeCount--;
	}

	return id;
}

int CutSheet::CreateShape(unsigned int id)
{
	ShapeHolder *found = nullptr;
	ShapeHolder *p = first;

	if (p)
	{
		do
		{
			if (p->id == id)
				found = p;
		} while ((p = p->next) && !found);
	}

	if (!found) {
		found = new ShapeHolder();
		
		found->next = nullptr;
		found->shape = new CutShape();
		found->id = id;

		if (shapeCount == 0) 
		{
			first = found;
			last = found;
			last->next = nullptr;
		}
		else 
		{
			last->next = found;
			last = found;
		}

		shapeCount++;
	}

	return found->id;
}

bool TestScan(CutScan* scan, int x, int y)
{
	CutSegmentList **scans = scan->GetSegments();
	int count = scan->GetLineCount();
	int cx = x - scan->GetCenter();

	int top = scan->GetTop();
	int bottom = scan->GetBottom();

	//if (cx < 0 || cx + scan->)

	for (int i = 0; i < count; i++, cx++)
	{
		//if (cx >= 0 &&)

	}

	return false;
}

bool CutSheet::TestFullScan(CutScan* scan, int x, int y)
{
	int count = scan->GetLineCount();
	CutSegmentList** segs = scan->GetSegments();
	int startLine = y + segs[0]->GetFirst()->GetLine();
	int sx, ex;

	if (startLine < 0 || startLine + count >= lines)
		return false;

	for (int i = 0; i < count; i++)
	{
		if (CutSegment *s = segs[i]->GetFirst()) 
		{
			CutSegmentList* used = &usedSpace[s->GetLine() + i];
			
			while (s)
			{
				sx = s->GetStart() + x;
				ex = s->GetEnd() + x;
				
				CutSegment *u = used->GetFirst();
				while (u)
				{
					if (u->Intersect(sx, ex))
						return false;

					u = u->GetNext();
				}

				s = s->GetNext();
			}
		}
	}

	return true;
}

#define _CRT_SECURE_NO_WARNINGS 1

void CutSheet::SetResult(int angle, int x, int y, int maxx)
{
	if (result.maxx > maxx)
	{
		result.x = x;
		result.y = y;
		result.maxx = maxx;
		result.angle = angle;
	}
	else if (result.maxx == maxx && y < result.y)
	{
		result.x = x;
		result.y = y;
		result.maxx = maxx;
		result.angle = angle;
	}

	foundPos = true;
}

void CutSheet::TestEndSpace(CutShape* shape)
{
	for (int i = 0; i < lines; i++)
	{
		int end = endPos[i];
		bool anglePass = false;

		for (int scanIndex = 0; scanIndex < MAX_ANGLES; scanIndex++)
		{
			CutScan *scan = shape->GetSortedScan(scanIndex);

			int x = 0;
			int y = i;
			
			int startLine = scan->GetStartLine(y);
			int lastLine = scan->GetLastLine(y);

			if (startLine >= 1 && lastLine < lines - 2)
			{
				int* l = endPos + startLine;
				int* s = scan->GetFirstPos();

				while (*s < INT_MAX)
				{
					if ((*s)+x <= *l)
					{
						x += *l - ((*s) + x);
					}

					s++, l++;
				}

    			SetResult(scan->GetAngle(), x, y, scan->GetRight() + x);
			}			
		}
	}
}

void CutSheet::TestFreeSpace(CutShape* shape)
{
	int x = 0;
	int y = 0;

	for (int i = 0; i < lines; i++)
	{
		CutSegmentList* free = &freeSpace[i];
		y = i;

		if (free->GetCount() > 0)
		{
			for (int angleIndex = 0; angleIndex < MAX_ANGLES; angleIndex++)
			{
				CutScan *scan = shape->GetSortedScan(angleIndex);

				int startLine = scan->GetStartLine(y);
				int lastLine = scan->GetLastLine(y);
				int lcount = scan->GetLineCount();

				bool pass = true;

				if (startLine > 0 && lastLine < lines-1)
				{
					CutSegment *freeSeg = free->GetFirst();

					bool found = false;

					while (freeSeg && !found)
					{
						int x = scan->GetMiddleLen() + 2 + freeSeg->GetStart();

						CutSegmentList* ul = &usedSpace[startLine];
						CutSegmentList **sl = scan->GetSegments();

						for (int k = 0; k < lcount && pass; k++, sl++, ul++)
						{
							CutSegmentList *ssl = *sl;
							CutSegment* usedSeg = ul->GetFirst();

							while (usedSeg && pass)
							{
								CutSegment *scanSeg = ssl->GetFirst();

								while (scanSeg && pass)
								{
									if (usedSeg->Intersect(scanSeg->GetStart() + x, scanSeg->GetEnd() + x))
									{
										pass = false;
									}

									scanSeg = scanSeg->GetNext();
								}

								usedSeg = usedSeg->GetNext();
							}
						}

						if (pass)
						{
							SetResult(scan->GetAngle(), x, y, scan->GetRight() + x);
							found = true;
						}

						freeSeg = freeSeg->GetNext();
					}
				}
			}
		}
	}
}

bool CutSheet::TestShape(CutShape* shape, CutTestResult *res)
{
	foundPos = false;
	memset(&result, 0, sizeof(result));

	result.maxx = INT_MAX;
	result.x = INT_MAX;
	result.y = INT_MIN;
	
	if (res != nullptr)
		res->resultOk = false;

	//TestFreeSpace(shape);

	if (!foundPos)
	{
		TestEndSpace(shape);
	}

	if (foundPos)
	{
		if (res != nullptr)
		{
			res->angle = result.angle;
			res->x = result.x;
			res->y = result.y;
			res->maxx = result.maxx;
			res->resultOk = true;
		}

		return true;
	}

	return false;
}

void CutSheet::Reset(int size)
{
	lines = size;

	if (freeSpace != nullptr)
		delete[] freeSpace;

	if (usedSpace != nullptr)
		delete[] usedSpace;

	if (endSpace != nullptr)
		delete[] endSpace;

	if (endPos != nullptr)
		delete[] endPos;

	freeSpace = new CutSegmentList[size];
	usedSpace = new CutSegmentList[size];
	endSpace = new CutSegment[size];
	endPos = new int[size];

	for (int i = 0; i < size; i++)
	{
		CutSegment* end = &endSpace[i];

		end->SetStart(0);
		end->SetEnd(INT_MAX);
		end->SetLine(i);

		endPos[i] = 0;
	}
}

void CutSheet::Plot(CutScan *scan, int x, int y)
{
	CutSegmentList **segs = scan->GetSegments();
	int count = scan->GetLineCount();
	
	for (int i = 0; i < count; i++)
	{
		CutSegmentList *src = segs[i];

		if (src->GetCount() > 0)
		{
			int destLine = y + src->GetFirst()->GetLine();

			if (destLine >= 0 && destLine < lines)
			{
				CutSegmentList *dest = usedSpace + destLine;
				src->Plot(dest, x);
				dest->Merge(minDist);
			}
		}
	}

	for (int i = 0; i < lines; i++)
	{
		CutSegmentList *free = &freeSpace[i];
		CutSegmentList *used = &usedSpace[i];
		CutSegment *end = &endSpace[i];

		free->Clear();
		CutSegment *s = used->GetFirst();
		CutSegment *last = nullptr;

		while (s)
		{
			CutSegment *next = s->GetNext();

			int sx = 0;
			int ex = 0;

			if (last != nullptr)
			{
				sx = last->GetEnd();
			}
			else
			{
				sx = 0;
			}

			ex = s->GetStart();

			if (sx > ex)
			{
				int tmp = sx;
				sx =  ex;
				ex = tmp;
			}

			if (sx < 0)
				sx = 0;

			if (ex - sx >= minDist)
			{
				free->AddSegment(i, sx, ex);
			}

			if (next == nullptr)
			{
				end->SetStart(s->GetEnd() + 1);
			}

			last = s;
			s = next;
		}

		endPos[i] = end->GetStart();		
	}
}

void CutSheet::Plot(CutShape* shape, int angle, int x, int y)
{
	if (shape != nullptr && angle >= 0)
	{
		CutScan* scan = shape->GetScan(angle);
		Plot(scan, x, y);
	} 
}

void DrawLine(HDC hDC, int sx, int sy, int ex, int ey)
{
	MoveToEx(hDC, sx, sy, NULL);
	LineTo(hDC, ex, ey);
}

#ifdef WIN32
void CutSheet::Render(HDC hdc, int x, int y)
{
	HPEN hLinePen1 = CreatePen(PS_SOLID, 1, RGB(200, 0, 0));
	HPEN hLinePen2 = CreatePen(PS_SOLID, 1, RGB(0, 200, 0));
	HPEN hLinePen3 = CreatePen(PS_SOLID, 1, RGB(0, 0, 200));	

	for (int i = 0; i < lines; i++)
	{
		CutSegmentList *free = &freeSpace[i];
		CutSegmentList *used = &usedSpace[i];
		CutSegment *end = &endSpace[i];

		CutSegment* s;

		SelectObject(hdc, hLinePen1);

		s = used->GetFirst();
		while (s)
		{
			DrawLine(hdc, s->GetStart() + x, i + y, s->GetEnd() + x, i + y);
			s = s->GetNext();
		}

		SelectObject(hdc, hLinePen2);

		if (free->GetCount() > 0) 
		{
			s = free->GetFirst();
			while (s)
			{
				DrawLine(hdc, s->GetStart() + x, i + y, s->GetEnd() + x, i + y);
				s = s->GetNext();
			}
		}

		SelectObject(hdc, hLinePen3);
		//DrawLine(hdc, end->GetStart() + x, i + y, 2000 + x, i + y);
	}
}
#endif