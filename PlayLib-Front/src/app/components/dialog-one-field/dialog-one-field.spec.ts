import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogOneField } from './dialog-one-field';

describe('DialogOneField', () => {
  let component: DialogOneField;
  let fixture: ComponentFixture<DialogOneField>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DialogOneField],
    }).compileComponents();

    fixture = TestBed.createComponent(DialogOneField);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
