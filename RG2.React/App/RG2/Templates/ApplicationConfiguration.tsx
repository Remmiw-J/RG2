﻿import * as React from 'react'
import { ApplicationConfigurationEntity } from '../RG2.Entities'
import { ValueLine, EntityLine, EntityCombo, EntityList, EntityDetail, EntityStrip, EntityRepeater, TypeContext, RenderEntity } from '@framework/Lines'
import { Tabs, Tab } from 'react-bootstrap';

export default function ApplicationConfiguration(p : { ctx: TypeContext<ApplicationConfigurationEntity> }){
  const ctx = p.ctx;
  return (
    <div>
      <ValueLine ctx={ctx.subCtx(a => a.environment)} />
      <Tabs id="appTabs">
        <Tab eventKey="tab" title={ctx.niceName(a => a.email)}>
          <RenderEntity ctx={ctx.subCtx(a => a.email)} />
          <EntityLine ctx={ctx.subCtx(a => a.emailSender)} />
        </Tab>
        <Tab eventKey="auth" title={ctx.niceName(a => a.authTokens)}>
          <RenderEntity ctx={ctx.subCtx(a => a.authTokens)} />
        </Tab>
        <Tab eventKey="webauthn" title={ctx.niceName(a => a.webAuthn)}>
          <RenderEntity ctx={ctx.subCtx(a => a.webAuthn)} />
        </Tab>
        <Tab eventKey="translation" title={ctx.niceName(a => a.translation)}>
            <RenderEntity ctx={ctx.subCtx(a => a.translation)} />
        </Tab>
      </Tabs>
    </div>
  );
}
