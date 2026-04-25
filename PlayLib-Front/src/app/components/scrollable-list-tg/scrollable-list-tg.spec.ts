import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScrollableListTg } from './scrollable-list-tg';

describe('ScrollableListTg', () => {
  let component: ScrollableListTg;
  let fixture: ComponentFixture<ScrollableListTg>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ScrollableListTg],
    }).compileComponents();

    fixture = TestBed.createComponent(ScrollableListTg);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
