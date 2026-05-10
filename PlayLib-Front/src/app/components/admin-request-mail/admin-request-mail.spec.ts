import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminRequestMail } from './admin-request-mail';

describe('AdminRequestMail', () => {
  let component: AdminRequestMail;
  let fixture: ComponentFixture<AdminRequestMail>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminRequestMail],
    }).compileComponents();

    fixture = TestBed.createComponent(AdminRequestMail);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
