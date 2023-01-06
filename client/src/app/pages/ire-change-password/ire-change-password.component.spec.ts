import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IreChangePasswordComponent } from './ire-change-password.component';

describe('IreChangePasswordComponent', () => {
  let component: IreChangePasswordComponent;
  let fixture: ComponentFixture<IreChangePasswordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IreChangePasswordComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IreChangePasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
