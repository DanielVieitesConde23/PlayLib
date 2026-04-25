import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScrollableListVg } from './scrollable-list-vg';

describe('ScrollableListVg', () => {
  let component: ScrollableListVg;
  let fixture: ComponentFixture<ScrollableListVg>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ScrollableListVg],
    }).compileComponents();

    fixture = TestBed.createComponent(ScrollableListVg);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
