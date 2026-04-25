import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VideogameLibrary } from './videogame-library';

describe('VideogameLibrary', () => {
  let component: VideogameLibrary;
  let fixture: ComponentFixture<VideogameLibrary>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VideogameLibrary],
    }).compileComponents();

    fixture = TestBed.createComponent(VideogameLibrary);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
