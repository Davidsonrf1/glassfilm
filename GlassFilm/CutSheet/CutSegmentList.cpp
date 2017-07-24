#include "CutSegmentList.h"


CutSegmentList::CutSegmentList()
{
	count = 0;
	first = 0;
	last = 0;
}

void CutSegmentList::Clear()
{
	if (count > 0) 
	{
		CutSegment *s = first;

		while (s != 0)
		{
			CutSegment *n = s->GetNext();
			delete s;
			s = n;
		}
	}

	count = 0;
}

void CutSegmentList::AddSegment(int line, int start, int end)
{
	CutSegment *seg = new CutSegment(line, start, end);

	if (count > 0) 
	{
		last->SetNext(seg);
		last = seg;
	}
	else 
	{
		last = seg;
		first = seg;
	}

	seg->SetNext(0);
	count++;
}

int CutSegmentList::GetCount()
{
	if (count == 0) 
	{
		first = nullptr;
		last = nullptr;
	}

	return count;
}

int CutSegmentList::Remove(CutSegment *seg) 
{
	CutSegment *s = first;
	CutSegment *l = nullptr;

	if (s)
	{
		if (count == 1)
		{
			if (first == seg) 
			{
				delete s;
				first = nullptr;
				last = nullptr;
				count = 0;
			}
		}
		else
		{
			do {
				if (seg == s)
				{
					if (seg == first)
					{
						first = s->GetNext();
					}
					else 
					{
						l->SetNext(seg->GetNext());
						
						if (seg == last)
							last = l;
					}

					count--;
					break;
				}

				l = s;
			} while (s = s->GetNext());
		}
	}

	return 0;
}

void CutSegmentList::Merge(int minDist)
{
	if (count <= 0)
		return;

	if (count > 1)
		int a = count;

	CutSegmentList nl;

	CutSegment** segs = new CutSegment*[count];
	CutSegment *s = nullptr;

	while (count > 0)
	{
		CutSegment *min = first;
		s = first->GetNext();

		while(s)
		{
			min = s->GetStart() < min->GetStart() ? s : min;
			s = s->GetNext();
		}

		nl.AddSegment(min->GetLine(), min->GetStart(), min->GetEnd());
		Remove(min);
	}

	Clear();
	
	AddSegment(nl.first->GetLine(), nl.first->GetStart(), nl.first->GetEnd());
	CutSegment *cur = first;
	s = nl.GetFirst()->GetNext();

	while (s)
	{
		int ce = cur->GetEnd();
		int ss = s->GetStart();
		int se = s->GetEnd();

		if (ss <= ce + minDist)
		{
			if (se > ce)
			{
				cur->SetEnd(se);
			}
		}
		else
		{
			AddSegment(s->GetLine(), s->GetStart(), s->GetEnd());
			cur = last;
		}

		s = s->GetNext();
	}

	nl.Clear();
}

void CutSegmentList::GetMinMax(int* min, int* max)
{
	*min = 0;
	*max = 0;

	CutSegment *s = first;

	if (s)
	{
		do {
			int i = s->GetStart();
			*min = *min > i ? i : *min;

			i = s->GetEnd();
			*max = *max > i ? i : *max;
		} while (s = s->GetNext());
	}
}

void CutSegmentList::Plot(CutSegmentList *dest, int x)
{
	CutSegment *s = first;

	while (s)
	{
		s->Plot(dest, x);
		s = s->GetNext();
	}
}