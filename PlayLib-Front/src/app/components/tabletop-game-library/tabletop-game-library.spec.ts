import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TabletopGameLibrary } from './tabletop-game-library';

describe('TabletopGameLibrary', () => {
  let component: TabletopGameLibrary;
  let fixture: ComponentFixture<TabletopGameLibrary>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TabletopGameLibrary],
    }).compileComponents();

    fixture = TestBed.createComponent(TabletopGameLibrary);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
