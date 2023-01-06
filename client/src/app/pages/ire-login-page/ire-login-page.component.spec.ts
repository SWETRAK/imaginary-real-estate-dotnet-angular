import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IreLoginPageComponent } from './ire-login-page.component';

describe('IreLoginPageComponent', () => {
  let component: IreLoginPageComponent;
  let fixture: ComponentFixture<IreLoginPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IreLoginPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IreLoginPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
