import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IreRegisterPageComponent } from './ire-register-page.component';

describe('IreRegisterPageComponent', () => {
  let component: IreRegisterPageComponent;
  let fixture: ComponentFixture<IreRegisterPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IreRegisterPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IreRegisterPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
