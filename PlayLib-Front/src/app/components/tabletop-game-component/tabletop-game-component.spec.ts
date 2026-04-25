import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TabletopGameComponent } from './tabletop-game-component';

describe('TabletopGameComponent', () => {
  let component: TabletopGameComponent;
  let fixture: ComponentFixture<TabletopGameComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TabletopGameComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(TabletopGameComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
